using System.Web.Mvc;

namespace BlockToDB.Areas.Dashboard.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}