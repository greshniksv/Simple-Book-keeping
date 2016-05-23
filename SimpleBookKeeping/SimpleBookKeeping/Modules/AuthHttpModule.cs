using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ninject;
using SimpleBookKeeping.Authentication;

namespace SimpleBookKeeping.Modules
{
    public class AuthHttpModule: IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.AuthenticateRequest += new EventHandler(this.Authenticate);
            context.BeginRequest += (new EventHandler(this.Application_BeginRequest));
            context.EndRequest += (new EventHandler(this.Application_EndRequest));
        }

        private void Authenticate(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            var auth = MvcApp.Kernel.Get<IAuthentication>();
            auth.HttpContext = context;
            context.User = auth.CurrentUser;
        }

        // Your BeginRequest event handler.
        private void Application_BeginRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            context.Response.Write("<h1><font color=red>HelloWorldModule: Beginning of Request</font></h1><hr>");
        }

        // Your EndRequest event handler.
        private void Application_EndRequest(Object source, EventArgs e)
        {
            HttpApplication application = (HttpApplication)source;
            HttpContext context = application.Context;
            context.Response.Write("<hr><h1><font color=red>HelloWorldModule: End of Request</font></h1>");
        }


        public void Dispose()
        {
        }
    }
}