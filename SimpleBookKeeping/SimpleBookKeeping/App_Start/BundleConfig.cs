using System.Web.Optimization;

namespace SimpleBookKeeping
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
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



        }
    }
}