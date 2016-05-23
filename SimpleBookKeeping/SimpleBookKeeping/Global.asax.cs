using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;

namespace SimpleBookKeeping
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static IKernel AppKernel;

        protected void Application_Start()
        {
            AppKernel = new StandardKernel(new NinjectConfig());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
