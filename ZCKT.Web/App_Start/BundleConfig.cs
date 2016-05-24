using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ZCKT.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts/modernizr").Include(
               "~/Content/vendors/modernizr.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts/vendors").Include(
                //jquery 2.1.2
                "~/Content/vendors/jquery/jquery.js",
                //bootstrap 3.3.6
                "~/Content/vendors/bootstrap/js/bootstrap.js",
                //toastr
                "~/Content/vendors/toastr/toastr.js",
                //angularjs 1.5.3
                "~/Content/vendors/angular/angular.js",
                "~/Content/vendors/angular-route/angular-route.js",
                "~/Content/vendors/angular-cookies/angular-cookies.js",
                "~/Content/vendors/angular-base64/angular-base64.js",
                "~/Content/vendors/angucomplete-alt/angucomplete-alt.min.js",
                //angularjs loading-bar
                "~/Content/vendors/angular-loading-bar/loading-bar.js",
                //jstree
                "~/Content/vendors/jstree/jstree.js",
                //jquery-gzoom
                "~/Content/vendors/zoom/jquery.zoom.js"
                ));

            bundles.Add(new StyleBundle("~/bundles/css/vendors").Include(
                //bootstrap 3.3.6
                "~/Content/vendors/bootstrap/css/bootstrap.css",
                "~/Content/vendors/bootstrap/css/bootstrap-theme.css",
                //font-awesome
                "~/Content/vendors/font-awesome/css/font-awesome.css",
                //toastr
                "~/Content/vendors/toastr/toastr.css",
                //angularjs loading-bar
                "~/Content/vendors/angular-loading-bar/loading-bar.css",
                //jstree
                "~/Content/vendors/jstree/themes/default/style.css",
                //site custom
                "~/Content/css/site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/scripts/spa").Include(
                "~/Scripts/spa/modules/common.core.js",
                "~/Scripts/spa/modules/common.ui.js",
                "~/Scripts/spa/app.js",
                //service
                "~/Scripts/spa/services/notificationService.js",
                "~/Scripts/spa/services/apiService.js",
                "~/Scripts/spa/services/membershipService.js",
                //directive
                //"~/Scripts/spa/directives/sideBar.directive.js",
                "~/Scripts/spa/directives/topBar.directive.js",
                "~/Scripts/spa/directives/bomTree.directive.js",
                "~/Scripts/spa/directives/collapseBar.directive.js",
                "~/Scripts/spa/directives/imgGzoom.directive.js",
                //controller
                "~/Scripts/spa/home/rootCtrl.js",
                "~/Scripts/spa/home/indexCtrl.js"
                ));
        }
    }
}