using System.Web;
using SimpleBookKeeping.Unility;

namespace SimpleBookKeeping.HttpHandler
{
    /// <summary>
    /// Summary description for DownloadFileHandler
    /// </summary>
    public class DownloadFileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var bigSize = context.Request["b"];
            var file = context.Request["f"];
            ImageStorage imageStorage = new ImageStorage();
            var fileInfo = imageStorage.Get(file, bigSize == null);

            context.Response.ContentType = "application/octet-stream";
            context.Response.AddHeader("Content-Disposition", "filename=\"" + file + "\";");
            context.Response.WriteFile(fileInfo.FullName);
        }

        public bool IsReusable => false;
    }
}