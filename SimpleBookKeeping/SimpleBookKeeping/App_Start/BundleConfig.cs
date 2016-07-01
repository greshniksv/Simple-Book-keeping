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

            // MessageBox
            bundles.Add(new ScriptBundle("~/messageBox.js").Include(
                     "~/Scripts/Class/messageBox.js"));
            
            // msgBox
            bundles.Add(new ScriptBundle("~/msgBox.js").Include(
                     "~/Scripts/Site/msgBox.js"));
            
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

            // Spend
            bundles.Add(new StyleBundle("~/spend.css").Include(
                      "~/Content/site/spend.less"));

            // Datepicker
            bundles.Add(new StyleBundle("~/datepicker.css").Include(
                      "~/Content/jquery.mobile.datepicker.theme.css").Include(
                      "~/Content/jquery.mobile.datepicker.css"));

            bundles.Add(new ScriptBundle("~/datepicker.js").Include(
                     "~/Scripts/jquery.mobile.datepicker.js").Include(
                     "~/Scripts/external/jquery-ui/datepicker.js"));

            // jQMProgressBar
            bundles.Add(new StyleBundle("~/jQMProgressBar.css").Include(
                      "~/Content/jQMProgressBar.css"));

            bundles.Add(new ScriptBundle("~/jQMProgressBar.js").Include(
                "~/Scripts/jQMProgressBar.js"));

        }

    }
}