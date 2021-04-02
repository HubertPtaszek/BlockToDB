using System.Web.Mvc;

namespace BlockToDB.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Creator");
        }

        public ActionResult About()
        {
            return View("About");
        }
    }
}