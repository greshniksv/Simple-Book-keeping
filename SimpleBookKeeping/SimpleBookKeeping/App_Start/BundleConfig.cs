using System.Web.Optimization;

namespace SimpleBookKeeping
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery.full").Include(
                       "~/Scripts/jquery-{version}.js",
                       "~/Scripts/jquery.validate*",
                       "~/Scripts/jquery.mobile-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery.mobile").Include(
                      "~/Scripts/jquery.mobile-{version}.js"));

            bundles.Add(new ScriptBundle("~/Css/jquery.mobile").Include(
                 "~/Content/jquery.mobile-1.4.5.css"));

            bundles.Add(new StyleBundle("~/Css/Login").Include(
                      "~/Content/site/login.less"));

            RegisterSiteBundle(bundles);
        }

        public static void RegisterSiteBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/login.js").Include(
                      "~/Scripts/Site/Login.js"));
        }

    }
}