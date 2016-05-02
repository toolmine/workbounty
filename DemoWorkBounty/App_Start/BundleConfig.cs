using System.Web;
using System.Web.Optimization;

namespace DemoWorkBounty
{
    public class BundleConfig
    {

        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                         "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

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


            bundles.Add(new ScriptBundle("~/assets/js").Include(
                     "~/assets/js/jquery.2.1.1.min.js",
                     "~/assets/js/jquery-search.js",
                     "~/assets/js/jq_script_src.js",
                      "~/assets/js/jquery_mobile_custom_start.js",
                      "~/assets/js/bootstrap.min.js",
                      "~/assets/js/jquery-ui.custom.min.js",
                      "~/assets/js/jquery-ui.min.js",
                      "~/assets/js/jquery.ui.touch-punch.min.js",
                      "~/assets/js/jquery.easypiechart.min.js",
                       "~/assets/js/jquery.sparkline.min.js",
                       "~/assets/js/jquery.flot.min.js",
                        "~/assets/js/ace-extra.min.js",
                        "~/assets/js/jquery.flot.pie.min.js",
                       "~/assets/js/jquery.flot.resize.min.js",
                         "~/assets/js/ace-elements.min.js",
                         "~/assets/js/ace.min.js",
                      "~/assets/js/jquery_onclick_success.js",
                      "~/assets/js/ace-extra.min.js",
                       "~/assets/js/jquery_onclick_success.js",
                         "~/assets/js/bootstrap-datepicker.min.js",
                         "~/assets/js/jquery-dfavourite.js",
                          "~/assets/js/jquery-vfavourite.js",
                         "~/assets/js/jquery_Bootstrap.js"

                ));


            bundles.Add(new StyleBundle("~/assets/css").Include(
                "~/assets/css/bootstrap.min.css",
                "~/assets/font-awesome/4.2.0/css/font-awesome.min.css",
                 "~/assets/fonts/fonts.googleapis.com.css",
                 "~/Content/themes/base/jquery.ui.all.css",
                "~/assets/css/ace.min.css",
                "~/assets/css/datepicker.min.css",
                "~/assets/css/jquery-ui.min.css",
                "~/assets/css/mandatory.css"
                ));


            bundles.Add(new ScriptBundle("~/layout/js").Include(
                "~/assets/js/jquery.min.js",
                "~/assets/js/ace-extra.min.js",
                "~/assets/js/jquery_mobile_custom_start.js",
                "~/assets/js/jq_script_src.js",
                 "~/assets/js/jquery.2.1.1.min.js",
                "~/assets/js/jquery-ui.custom.min.js",
                "~/assets/js/jquery.ui.touch-punch.min.js",
                "~/assets/js/bootstrap.min.js",
                "~/assets/js/jquery_mobile_custom_start.js",
                "~/assets/js/jq_script_src.js",
                "~/assets/js/jquery_mobile_custom_start.js",
                 "~/assets/js/jquery.easypiechart.min.js",
                "~/assets/js/jquery.sparkline.min.js",
                "~/assets/js/jquery.flot.min.js",
                "~/assets/js/jquery.flot.resize.min.js",
                "~/assets/js/bootstrap-datepicker.min.js",
                "~/assets/js/ace-elements.min.js",
                "~/assets/js/ace.min.js",
                "~/assets/js/bootstrap-datepicker.min.js",
                 "~/assets/js/jquery_Bootstrap.js"
                ));


            bundles.IgnoreList.Clear();


        }



    }
}