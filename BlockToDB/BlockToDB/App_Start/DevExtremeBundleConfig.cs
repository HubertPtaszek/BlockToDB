using System.Web.Optimization;

namespace BlockToDB
{
    public class DevExtremeBundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            var scriptBundle = new ScriptBundle("~/Scripts/DevExtremeBundle");
            var styleBundle = new StyleBundle("~/Content/DevExtremeBundle");
            scriptBundle
                .Include("~/Scripts/jszip.js");

            scriptBundle
                .Include("~/Scripts/dx.all.js")
                .Include("~/Scripts/aspnet/dx.aspnet.data.js")
                .Include("~/Scripts/aspnet/dx.aspnet.mvc.js");

            styleBundle
                .Include("~/Content/dx.common.css")
                .Include("~/Content/CMS.theme.css");

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

            var scriptBundlePL = new ScriptBundle("~/Scripts/DevExtremeBundlePL");
            scriptBundlePL.Include("~/Scripts/globalize.load.pl.js");
            scriptBundlePL.Include("~/Scripts/dx.all.pl.js");
            bundles.Add(scriptBundlePL);

            var scriptBundleDE = new ScriptBundle("~/Scripts/DevExtremeBundleDE");
            scriptBundleDE.Include("~/Scripts/globalize.load.de.js");
            scriptBundleDE.Include("~/Scripts/localization/dx.messages.de.js");
            bundles.Add(scriptBundleDE);

            var scriptBundleEN = new ScriptBundle("~/Scripts/DevExtremeBundleEN");
            scriptBundleEN.Include("~/Scripts/globalize.load.en.js");
            bundles.Add(scriptBundleEN);
        }
    }
}