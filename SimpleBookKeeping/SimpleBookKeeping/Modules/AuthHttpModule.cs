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
            //context.BeginRequest += (new EventHandler(this.Application_BeginRequest));
            //context.EndRequest += (new EventHandler(this.Application_EndRequest));
        }

        private void Authenticate(Object source, EventArgs e)
        {
            HttpApplication app = (HttpApplication)source;
            HttpContext context = app.Context;

            var auth = MvcApp.Kernel.Get<IAuthentication>();
            auth.HttpContext = context;
            context.User = auth.CurrentUser;
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
        }


        public void Dispose()
        {
        }
    }
}