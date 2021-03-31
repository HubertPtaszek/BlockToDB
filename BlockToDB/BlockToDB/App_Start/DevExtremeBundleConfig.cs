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
                .Include("~/Scripts/aspnet/dx.aspnet.data.js");

            styleBundle
                .Include("~/Content/dx.common.css")
                .Include("~/Content/blocktodb.theme.css");

            bundles.Add(scriptBundle);
            bundles.Add(styleBundle);

            var scriptBundlePL = new ScriptBundle("~/Scripts/DevExtremeBundlePL");
            scriptBundlePL.Include("~/Scripts/globalize.load.pl.js");
            scriptBundlePL.Include("~/Scripts/dx.all.pl.js");
            bundles.Add(scriptBundlePL);
        }
    }
}