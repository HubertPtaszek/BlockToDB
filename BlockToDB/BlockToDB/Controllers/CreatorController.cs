using BlockToDB.Application;
using BlockToDB.Utils;
using Ninject;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace BlockToDB.Areas.Dashboard.Controllers
{
    public class CreatorController : AppController
    {
        #region Dependencies

        [Inject]
        public IBlockToDBService BlockToDBService { get; set; }

        #endregion Dependencies

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GenerateScriptFile(BlockToDBGenerateVM model)
        {
            if (ModelState.IsValid)
            {
                int fileId = BlockToDBService.GenerateScript(model);
                return new JsonResult()
                {
                    Data = new { Id = fileId }
                };
            }
            return new HttpStatusCodeResult(HttpStatusCode.Created, "Generated");
        }

        [HttpPost]
        public ActionResult CreateRemoteDataBase(BlockToDBGenerateRemoteVM model)
        {
            if (ModelState.IsValid)
            {
                string status = "";
                return new JsonResult()
                {
                    Data = new { Status = status }
                };
            }
            return new HttpStatusCodeResult(HttpStatusCode.Created);
        }

        [HttpPost]
        public ActionResult SaveSchemaData(BlockToDBAddVM model)
        {
            if (ModelState.IsValid)
            {
                BlockToDBService.Add(model);
                return new HttpStatusCodeResult(HttpStatusCode.Created, "Added");
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        [HttpPost]
        public ActionResult ExportSchemaData(BlockToDBAddVM model)
        {
            if (ModelState.IsValid)
            {
                int fileId = BlockToDBService.Add(model);
                return new JsonResult()
                {
                    Data = new { Id = fileId }
                };
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult Download(int id, bool isScript)
        {
            DownloadVM model = BlockToDBService.DownloadFile(id, isScript);
            if (model != null)
            {
                return File(Encoding.UTF8.GetBytes(model.Content), model.ContentType, model.Name);
            }
            return RedirectToAction("Index");
        }
    }
}