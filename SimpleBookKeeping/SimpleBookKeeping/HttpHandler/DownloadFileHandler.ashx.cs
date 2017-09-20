using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimpleBookKeeping.HttpHandler
{
    /// <summary>
    /// Summary description for DownloadFileHandler
    /// </summary>
    public class DownloadFileHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            context.Response.Write("Hello World");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}