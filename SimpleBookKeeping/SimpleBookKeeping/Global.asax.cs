using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Ninject;

namespace SimpleBookKeeping
{
    public class MvcApp : System.Web.HttpApplication
    {
        public static IKernel Kernel;

        protected void Application_Start()
        {
            Kernel = new StandardKernel(new NinjectConfig());
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMaps();
            GlobalFiltersConfig.RegisterGlobalFilters(GlobalFilters.Filters);
        }
    }
}
