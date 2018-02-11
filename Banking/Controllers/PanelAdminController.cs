using System.Web.Mvc;

namespace Banking.Controllers
{
    [Authorize(Roles = "admin")]
    public class PanelAdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}