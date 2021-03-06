﻿using System.Web;
using System.Web.Optimization;

namespace Esthetic
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/unobtrusive").Include(
                        "~/Scripts/jquery.unobtrusive*"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                      "~/Scripts/dropzone/dropzone.min.js",
                      "~/Scripts/photoswipe/photoswipe.min.js",
                      "~/Scripts/photoswipe/photoswipe-ui-default.js",
                      "~/Scripts/slick/slick.min.js",
                      "~/Scripts/app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Scripts/dropzone/basic.min.css",
                      "~/Scripts/dropzone/dropzone.min.css",
                      "~/Scripts/photoswipe/photoswipe.css",
                      "~/Scripts/photoswipe/default-skin/default-skin.css",
                      "~/Scripts/photoswipe/photoswipe.css",
                      "~/Scripts/slick/slick.css",
                      "~/Scripts/slick/slick-theme.css",
                      "~/Content/site.css"));
        }
    }
}
