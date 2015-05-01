using System.Web.Optimization;

namespace MachineKeyGenerator.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            /*
             *  Script bundles
             * --------------------------------------------------------------------------------- */

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/scripts/jquery.unobtrusive*",
                        "~/scripts/jquery.validate.min.js",
                        "~/scripts/jquery.validate.unobtrusive.min.js",
                        "~/scripts/jquery.validate.unobtrusive.bootstrap.*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/scripts/bootstrap.*",
                        "~/scripts/bootbox.*"));

            bundles.Add(new ScriptBundle("~/bundles/plugins").Include(
                        "~/scripts/prettify.*",
                        "~/scripts/plugins.*"));


            /*
             *  Style bundles
             * --------------------------------------------------------------------------------- */

            bundles.Add(new StyleBundle("~/content/css").Include(
                        "~/content/bootstrap.*",
                        "~/content/font-awesome.*",
                        "~/content/google.prettify.*",
                        "~/content/mkg.*"));


            BundleTable.EnableOptimizations = true;
        }
    }
}
