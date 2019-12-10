using System.Web;
using System.Web.Optimization;

namespace SearchForPerfectHome
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                      "~/Scripts/jquery-ui.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/carousal").Include(
                "~/Scripts/Slick/slick.min.js",
                "~/Scripts/Home/carousal.js"));

            #region Style bundle
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/base-admin-3.css",
                      "~/Content/base-admin-3-responsive.css",
                      "~/Content/jquery-ui.theme.min.css"));

            bundles.Add(new StyleBundle("~/Content/logon").Include(
                        "~/Content/logon.css"));

            bundles.Add(new StyleBundle("~/Content/property").Include(
                        "~/Content/Property.css"));

            bundles.Add(new StyleBundle("~/Content/carousal").Include(
                "~/Content/Slick/slick.css",
                "~/Content/Slick/slick-theme.css"));
            #endregion

        }
    }
}
