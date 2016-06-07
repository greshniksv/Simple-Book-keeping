using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.Practices.Unity;

namespace SimpleBookKeeping
{
    public class MvcApp : System.Web.HttpApplication
    {
        public static UnityContainer Unity;

        protected void Application_Start()
        {
            Unity = new UnityContainer();
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AutoMapperConfig.RegisterMaps();
            GlobalFiltersConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            UnityConfig.RegisterTypes(Unity);
        }
    }
}
