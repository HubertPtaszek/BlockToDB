using System.Web.Optimization;

namespace BlockToDB
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region scriptBundle

            var scriptBundle = new ScriptBundle("~/Scripts/jqueryGlobalize");
            scriptBundle.Include(
                 "~/Scripts/jquery-{version}.js",
                 "~/Scripts/jquery.scrollbar.min.js",
                 "~/Scripts/js.cookie.js",
                 "~/Scripts/jquery.smartCart.js",
                 "~/Scripts/jquery.unobtrusive-ajax.min.js",
                   "~/Scripts/main.js"

                 );
            // CLDR scripts
            scriptBundle
                .Include("~/Scripts/cldr.js")
                .Include("~/Scripts/cldr/event.js")
                .Include("~/Scripts/cldr/supplemental.js")
                .Include("~/Scripts/cldr/unresolved.js");

            // Globalize 1.x
            scriptBundle
                .Include("~/Scripts/globalize.js")
                .Include("~/Scripts/globalize/message.js")
                .Include("~/Scripts/globalize/number.js")
                .Include("~/Scripts/globalize/currency.js")
                .Include("~/Scripts/globalize/date.js")
                .Include("~/Scripts/globalize.extensions.js");

            bundles.Add(scriptBundle);

            #endregion scriptBundle

            #region Modernizr

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            #endregion Modernizr

            #region bootstrap

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.bundle.min.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/main.css"));

            #endregion bootstrap

            #region jquery

            bundles.Add(new ScriptBundle("~/Scripts/jqueryBase64").Include(
                "~/Scripts/jquery.base64.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            #endregion jquery
        }
    }
}