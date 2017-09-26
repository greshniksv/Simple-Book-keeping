using System;
using System.IO;
using System.ServiceModel.Channels;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using NHibernate.Util;
using SimpleBookKeeping.Database;
using SimpleBookKeeping.Database.Entities;
using SimpleBookKeeping.Unility;

namespace SimpleBookKeeping.HttpHandler
{
    /// <summary>
    /// Summary description for UploadFileHandler
    /// </summary>
    public class UploadFileHandler : IHttpHandler
    {
        private static BufferManager bufferManager;
        private readonly object _obj = new object();

        private BufferManager BufferManager
        {
            get
            {
                if (bufferManager == null)
                {
                    lock (_obj)
                    {
                        if (bufferManager == null)
                        {
                            bufferManager = BufferManager.CreateBufferManager(9000000, 300000);
                        }
                    }
                }
                return bufferManager;
            }
        }

        public void ProcessRequest(HttpContext context)
        {
            var tempDir = System.Web.Hosting.HostingEnvironment.MapPath(@"~/App_Data/temp/");

            if (tempDir == null)
            {
                throw new Exception("Can't get temp directory");
            }

            if (!Directory.Exists(tempDir))
            {
                Directory.CreateDirectory(tempDir);
            }

            try
            {
                var spendId = context.Request["Spend"];
                if (string.IsNullOrEmpty(spendId))
                {
                    throw new Exception("Spend Id not set");
                }

                var imageId = context.Request["FileName"];
                if (string.IsNullOrEmpty(imageId))
                {
                    throw new Exception("Image name not set");
                }
               
                int chunk = context.Request["chunk"] != null ? int.Parse(context.Request["chunk"]) : 0;
                int chunks = context.Request["chunks"] != null ? int.Parse(context.Request["chunks"]) : 0;
                string fileName = context.Request["name"] ?? string.Empty;
                string tempFile = tempDir + imageId;
                HttpPostedFile fileUpload = context.Request.Files[0];

                using (FileStream fs = new FileStream(tempFile, chunk == 0 ? FileMode.Create : FileMode.Append))
                {
                    var bufSize = (int)fileUpload.InputStream.Length;
                    byte[] buffer = BufferManager.TakeBuffer(bufSize);
                    try
                    {
                        fileUpload.InputStream.Read(buffer, 0, bufSize);
                        fs.Write(buffer, 0, bufSize);
                        fs.Close();
                    }
                    finally
                    {
                        BufferManager.ReturnBuffer(buffer);
                    }
                }
                
                // If upload is finished
                if (chunks > 0 && chunk == chunks - 1)
                {
                    using (var session = Db.Session)
                    using (var transaction = session.BeginTransaction())
                    {
                        try
                        {
                            var spendGuid = Guid.Parse(spendId);
                            Spend spend = session.QueryOver<Spend>().Where(x => x.Id == spendGuid).List().First() as Spend;
                            if (spend != null)
                            {
                                spend.Image = imageId;
                                session.SaveOrUpdate(spend);
                            }
                            transaction.Commit();
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }

                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(100);
                        new FileStorage().Move(imageId, tempFile);
                    });
                }

                context.Response.ContentType = "text/plain";
                context.Response.Write("Success");
            }
            catch (Exception e)
            {
                context.Response.ContentType = "text/plain";
                context.Response.Write("Error: " + e);
                throw;
            }
        }

        public bool IsReusable => false;
    }
}