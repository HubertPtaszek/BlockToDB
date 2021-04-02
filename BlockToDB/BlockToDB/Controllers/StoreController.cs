using BlockToDB.Utils;
using System.Web.Mvc;

namespace BlockToDB.Areas.Dashboard.Controllers
{
    public class StoreController : AppController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}