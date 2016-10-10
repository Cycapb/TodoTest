using System.Web.Optimization;

namespace TodoWEB
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery-ajax").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/locales/bootstrap-datepicker.ru.min.js",
                "~/Scripts/CalendarScripts.js",
                "~/Scripts/Scripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetime-datepicker").Include(
                "~/Scripts/bootstrap-datepicker.js",
                "~/Scripts/locales/bootstrap-datepicker.ru.min.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap-datepicker/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/bootstrap-theme.css",
                "~/Content/bootstrap-datepicker.css",
                "~/Content/ErrorStyles.css",
                "~/Content/Site.css")
                );
        }
    }
}