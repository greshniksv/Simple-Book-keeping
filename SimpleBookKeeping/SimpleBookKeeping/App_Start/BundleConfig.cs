using System.Web.Optimization;

namespace SimpleBookKeeping
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
            //           "~/Scripts/jquery-2.1.4.js",
            //           "~/Scripts/jquery.validate*",
            //           "~/Scripts/jquery.mobile-1.4.5.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery").Include(
                      "~/Scripts/jquery-2.1.4.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery.validate").Include(
                      "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/Scripts/jquery.mobile").Include(
                      "~/Scripts/jquery.mobile-1.4.5.js"));

            //bundles.Add(new ScriptBundle("~/Content/jquery").Include(
            //     "~/Content/jquery.mobile-1.4.5.css",
            //     "~/Content/messages.css"));

            bundles.Add(new StyleBundle("~/Content/jquery.mobile").Include(
                 "~/Content/jquery.mobile-1.4.5.min.css"));

            RegisterSiteBundle(bundles);
        }

        private static void RegisterSiteBundle(BundleCollection bundles)
        {
            // Login
            
            bundles.Add(new ScriptBundle("~/Scripts/site/login").Include(
                      "~/Scripts/site/login.js"));
            
            bundles.Add(new StyleBundle("~/Content/site/login").Include(
                      "~/Content/site/login.css"));

            // Layout
            bundles.Add(new ScriptBundle("~/Scripts/Site/layout").Include(
                     "~/Scripts/Site/layout.js"));

            bundles.Add(new StyleBundle("~/Content/site/layout").Include(
                      "~/Content/site/layout.css"));

            // Animate
            bundles.Add(new ScriptBundle("~/Scripts/Site/animate").Include(
                     "~/Scripts/Site/animate.js"));
            
            // msgBox
            bundles.Add(new ScriptBundle("~/Scripts/Site/msgBox").Include(
                     "~/Scripts/Site/msgBox.js"));
            
            // Home
            bundles.Add(new StyleBundle("~/Content/site/home").Include(
                      "~/Content/site/home.css"));

            // Plan
            bundles.Add(new StyleBundle("~/Content/site/plan").Include(
                      "~/Content/site/plan.css"));

            // Common
            bundles.Add(new StyleBundle("~/Content/site/common").Include(
                      "~/Content/site/common.css"));
            // Cost
            bundles.Add(new StyleBundle("~/Content/site/cost").Include(
                      "~/Content/site/cost.css"));

            // Spend
            bundles.Add(new StyleBundle("~/Content/site/spend").Include(
                      "~/Content/site/spend.css"));

            // Datepicker
            bundles.Add(new StyleBundle("~/Content/jquery.mobile.datepicker").Include(
                      "~/Content/jquery.mobile.datepicker.theme.css").Include(
                      "~/Content/jquery.mobile.datepicker.css"));

            bundles.Add(new ScriptBundle("~/Scripts/datepicker").Include(
                     "~/Scripts/jquery.mobile.datepicker.js").Include(
                     "~/Scripts/external/jquery-ui/datepicker.js"));

            // jQMProgressBar
            //bundles.Add(new StyleBundle("~/jQMProgressBar.css").Include(
            //          "~/Content/jQMProgressBar.css"));

            //bundles.Add(new ScriptBundle("~/jQMProgressBar.js").Include(
            //    "~/Scripts/jQMProgressBar.js"));

        }

    }
}