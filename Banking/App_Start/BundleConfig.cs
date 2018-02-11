using System.Web.Optimization;

namespace Banking
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            //USER CSS
            bundles.Add(new StyleBundle("~/UserCSS").Include(
                         "~/Content/User/bootstrap.min.css",
                         "~/Content/User/sticky-footer-navbar.css"));


            //USER JS
            bundles.Add(new ScriptBundle("~/UserJS").Include(
                         "~/Scripts/User/jquery-3.1.1.slim.min.js",
                         "~/Scripts/User/tether.min.js",
                         "~/Scripts/User/bootstrap.min.js"));

            //ADMIN CSS
            bundles.Add(new StyleBundle("~/AdminCSS").Include(
                         "~/Content/Admin/main.css",
                         "~/Content/Admin/font-awesome.min.css"));

            //ADMIN JS
            bundles.Add(new ScriptBundle("~/AdminJS").Include(
                         "~/Scripts/Admin/jquery-3.2.1.min.js",
                         "~/Scripts/Admin/popper.min.js",
                         "~/Scripts/Admin/bootstrap.min.js",
                         "~/Scripts/Admin/main.js",
                         "~/Scripts/Admin/pace.min.js"));

            //DATATABLES CSS
            bundles.Add(new StyleBundle("~/DatatablesCSS").Include(
                         "~/Content/Admin/dataTables.bootstrap4.min.css"));

            //DATATABLES JS
            bundles.Add(new ScriptBundle("~/DatatablesJS").Include(
                         "~/Scripts/Admin/jquery.dataTables.min.js",
                         "~/Scripts/Admin/dataTables.bootstrap4.min.js"));

            //JQUERY VALIDATE
            bundles.Add(new ScriptBundle("~/Validate").Include(
                         "~/Scripts/Admin/jquery.validate.min.js",
                         "~/Scripts/Admin/jquery.validate.unobtrusive.min.js"));

            //SWEETALERT
            bundles.Add(new ScriptBundle("~/SweetAlert").Include(
                         "~/Scripts/Admin/sweetalert.min.js"));

            //DATEPICKER
            bundles.Add(new ScriptBundle("~/DatePicker").Include(
                         "~/Scripts/Admin/bootstrap-datepicker.min.js",
                         "~/Scripts/Admin/bootstrap-datepicker.pl.min.js"));

            //SELECT2
            bundles.Add(new ScriptBundle("~/Select2").Include(
                         "~/Scripts/Admin/select2.min.js"));
        }
    }
}
