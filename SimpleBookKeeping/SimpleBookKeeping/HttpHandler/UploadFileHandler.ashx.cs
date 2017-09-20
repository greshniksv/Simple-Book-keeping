using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using SimpleBookKeeping.Unility;

namespace SimpleBookKeeping.HttpHandler
{
    /// <summary>
    /// Summary description for UploadFileHandler
    /// </summary>
    public class UploadFileHandler : IHttpHandler
    {
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
                var spendId = context.Request["spend"];
                if (spendId == null)
                {
                    throw new Exception("Spend id not set");
                }
               
                int chunk = context.Request["chunk"] != null ? int.Parse(context.Request["chunk"]) : 0;
                int chunks = context.Request["chunks"] != null ? int.Parse(context.Request["chunks"]) : 0;
                string fileName = context.Request["name"] ?? string.Empty;
                string path = tempDir + spendId;
                HttpPostedFile fileUpload = context.Request.Files[0];

                using (FileStream fs = new FileStream(path, chunk == 0 ? FileMode.Create : FileMode.Append))
                {
                    var bufSize = (int)fileUpload.InputStream.Length;
                    byte[] buffer = new byte[bufSize];
                    //context.Request.InputStream.Read(buffer, 0, bufSize);
                    fileUpload.InputStream.Read(buffer, 0, bufSize);
                    fs.Write(buffer, 0, bufSize);
                    fs.Close();
                }
                
                // If upload is finished
                if (chunks > 0 && chunk == chunks - 1)
                {
                    Task.Factory.StartNew(() =>
                    {
                        Thread.Sleep(500);
                        new FileStorage().Move(spendId, path);
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