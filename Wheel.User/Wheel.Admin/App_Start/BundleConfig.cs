using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace Wheel.Admin.App_Start
{
    public class BundleConfig
    { // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/TyreBrandScripts").Include("~/Scripts/TyreBrand_Crud.js"));
            bundles.Add(new ScriptBundle("~/bundles/TyreModelScripts").Include("~/Scripts/TyreModel_Crud.js"));
            bundles.Add(new ScriptBundle("~/bundles/AdminScripts").Include("~/Content/assets/js/common.min.js",
                "~/Content/assets/js/uikit_custom.min.js", 
                "~/Content/assets/js/altair_admin_common.min.js",
                "~/Content/assets/js/kendoui_custom.min.js",
                "~/Content/assets/js/pages/kendoui.min.js",
                "~/Content/bower_components/peity/jquery.peity.min.js",
                "~/Content/bower_components/countUp.js/dist/countUp.min.js",
                "~/Content/bower_components/handlebars/handlebars.min.js",
                "~/Content/assets/js/custom/handlebars_helpers.min.js",
                "~/Content/bower_components/clndr/clndr.min.js",
                "~/Content/assets/js/pages/components_notifications.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/TableSorter").Include(
                "~/Content/bower_components/tablesorter/dist/js/jquery.tablesorter.min.js",
                "~/Content/bower_components/tablesorter/dist/js/jquery.tablesorter.widgets.min.js",
                "~/Content/bower_components/tablesorter/dist/js/widgets/widget-alignChar.min.js",
                "~/Content/bower_components/tablesorter/dist/js/widgets/widget-columnSelector.min.js",
                "~/Content/bower_components/tablesorter/dist/js/widgets/widget-print.min.js",
                "~/Content/bower_components/tablesorter/dist/js/extras/jquery.tablesorter.pager.min.js",
                "~/Content/bower_components/ion.rangeslider/js/ion.rangeSlider.min.js",
                "~/Content/bower_components/jquery-ui/jquery-ui.min.js",
                "~/Content/bower_components/jtable/lib/jquery.jtable.min.js"));
            BundleTable.EnableOptimizations = true;
        }
        
    }
}