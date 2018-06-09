﻿using System.Web;
using System.Web.Optimization;

namespace WebSite
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            // About CDNs and fallbacks, see
            // http://www.hanselman.com/blog/CDNsFailButYourScriptsDontHaveToFallbackFromCDNToLocalJQuery.aspx
            //
            // CDN performance and reliability
            // http://www.cdnperf.com/
            //
            // Most popular CDN
            // http://trends.builtwith.com/cdn


            bundles.UseCdn = true;
            //########### BundleTable.EnableOptimizations = true; //force optimization while debugging

            var jquery = new ScriptBundle("~/bundles/jquery", "//ajax.aspnetcdn.com/ajax/jquery/jquery-1.11.0.min.js").Include(
                    "~/Scripts/jquery/jquery-{version}.js");
            jquery.CdnFallbackExpression = "window.jQuery";
            bundles.Add(jquery);

            // --------------------

            var bootstrap = new ScriptBundle("~/bundles/bootstrap", "//netdna.bootstrapcdn.com/bootstrap/3.1.1/js/bootstrap.min.js").Include(
                    "~/Scripts/bootstrap-3.1.1/bootstrap.js");
            jquery.CdnFallbackExpression = "$.fn.modal";
            bundles.Add(bootstrap);

            // --------------------

            var site = new ScriptBundle("~/bundles/site")
                .Include("~/Scripts/js-cookie/js.cookie.js")
                .Include("~/Scripts/commontabs.js")
                .Include("~/Scripts/netselector.js")
                .Include("~/Scripts/menu.js");
            bundles.Add(site);

            // --------------------

            var cssBundle = new StyleBundle("~/Content/css");
            cssBundle.Include("~/Content/bootstrap-3.1.1/bootstrap.css");
            cssBundle.Include("~/Content/Site/Site.css");
            cssBundle.Include("~/Content/Site/commonTabs.css");
            bundles.Add(cssBundle);
        }
    }
}