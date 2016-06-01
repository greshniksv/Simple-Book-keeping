using System.Web.Optimization;

namespace SimpleBookKeeping
{
    public static class BundleConfig
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

            bundles.Add(new ScriptBundle("~/jquery.mobile.css").Include(
                 "~/Content/jquery.mobile-{version}.css",
                 "~/Content/messages.less"));
            
            RegisterSiteBundle(bundles);
        }

        private static void RegisterSiteBundle(BundleCollection bundles)
        {
            // Login

            bundles.Add(new ScriptBundle("~/login.js").Include(
                      "~/Scripts/Site/Login.js"));

            bundles.Add(new StyleBundle("~/login.css").Include(
                      "~/Content/site/login.less"));

            // Layout

            bundles.Add(new ScriptBundle("~/layout.js").Include(
                     "~/Scripts/Site/layout.js"));

            bundles.Add(new StyleBundle("~/layout.css").Include(
                      "~/Content/site/layout.less"));

            // Animate
            bundles.Add(new ScriptBundle("~/animate.js").Include(
                     "~/Scripts/Site/animate.js"));

            // mPopup
            bundles.Add(new ScriptBundle("~/mPopup.js").Include(
                     "~/Scripts/mPopup.jquery.js"));

            bundles.Add(new StyleBundle("~/mPopup.css").Include(
                      "~/Content/mPopup.css"));

            // Home
            bundles.Add(new StyleBundle("~/home.css").Include(
                      "~/Content/site/home.less"));

            // Plan
            bundles.Add(new StyleBundle("~/plan.css").Include(
                      "~/Content/site/plan.less"));

            // Common
            bundles.Add(new StyleBundle("~/common.css").Include(
                      "~/Content/site/common.less"));
            // Cost
            bundles.Add(new StyleBundle("~/cost.css").Include(
                      "~/Content/site/cost.less"));

        }

    }
}