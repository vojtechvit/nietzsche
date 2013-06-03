using System.Web.Optimization;
using System.Linq;

namespace NietzscheBiography.WebSite
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Styles
            bundles.Add(new StyleBundle("~/css/common").Include(
                "~/content/less/bootstrap/bootstrap.css",
                "~/content/less/bootstrap/responsive.css",
                "~/content/less/bootstrap-mvc-validation.css",
                "~/content/less/body.css"));

            bundles.Add(new StyleBundle("~/css/timeline").Include(
                    "~/content/less/timeline.css",
                    "~/content/less/timeline.light.css"));
            
            bundles.Add(new StyleBundle("~/css/jquery/themes/base").Include(
                        "~/content/themes/base/jquery.ui.core.css",
                        "~/content/themes/base/jquery.ui.resizable.css",
                        "~/content/themes/base/jquery.ui.selectable.css",
                        "~/content/themes/base/jquery.ui.accordion.css",
                        "~/content/themes/base/jquery.ui.autocomplete.css",
                        "~/content/themes/base/jquery.ui.button.css",
                        "~/content/themes/base/jquery.ui.dialog.css",
                        "~/content/themes/base/jquery.ui.slider.css",
                        "~/content/themes/base/jquery.ui.tabs.css",
                        "~/content/themes/base/jquery.ui.datepicker.css",
                        "~/content/themes/base/jquery.ui.progressbar.css",
                        "~/content/themes/base/jquery.ui.theme.css"));

            // Scripts
            bundles.Add(new ScriptBundle("~/js/common").Include(
                "~/scripts/jquery-{version}.js",
                "~/scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/js/timeline").Include(
                "~/scripts/timeline.js",
                "~/scripts/functions.timeline.js"));

            bundles.Add(new ScriptBundle("~/js/map").Include(
                "~/scripts/jquery.ui.map.js",
                "~/scripts/functions.map.js"));

            bundles.Add(new ScriptBundle("~/js/jquery-ui").Include(
                        "~/scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/js/jquery-validate").Include(
                        "~/scripts/jquery.unobtrusive*",
                        "~/scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/js/modernizr").Include(
                        "~/scripts/modernizr-*"));
        }
    }
}