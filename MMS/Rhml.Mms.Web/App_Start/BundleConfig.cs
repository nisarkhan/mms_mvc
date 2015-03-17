using System;
using System.Web;
using System.Web.Optimization;

namespace Rhml.Mms.Web
{
    public static class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            if (bundles == null)
            {
                throw new ArgumentNullException("bundles");
            }
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);       // explicitly setup ignore list from default

            // common scripting ---------------------------------------------------------------------------
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.9.1.js",
                        "~/Scripts/jquery-migrate-1.1.1.js",
                        "~/Scripts/jquery.cookie.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/Kendo").Include(
                        "~/Scripts/kendo/2012.3.1315/kendo.all.min.js",
                        "~/Scripts/kendo/2012.3.1315/kendo.aspnetmvc.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                        "~/Scripts/knockout-2.2.1.debug.js",
                        "~/Scripts/knockout.changetracker.js",
                        "~/Scripts/knockout.mapping-latest.debug.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/appUtil").Include(
                        "~/Scripts/application/appUtilities.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            // application specific scripting ----------------------------------------------------------
            // note: bundle together all scripts that share the same controller     


            // common styling ---------------------------------------------------------------------------
            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/normalize.css",
                        "~/Content/bootstrap.css",
                        "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Kendo/css").Include(
                "~/Content/kendo/2012.3.1315/kendo.common.min.css",
                "~/Content/kendo/2012.3.1315/kendo.dataviz.min.css",
                "~/Content/kendo/2012.3.1315/kendo.bootstrap.min.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));        
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignore)
        {
            if (ignore == null)
            {
                throw new ArgumentNullException("ignore");
            }
            ignore.Ignore("*.intellisense.js");
            ignore.Ignore("*-vsdoc.js");
            ignore.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignore.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignore.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}