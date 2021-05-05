using BlockToDB.Application;
using BlockToDB.Utils;
using Ninject;
using System.Net;
using System.Web.Mvc;

namespace BlockToDB.Areas.Dashboard.Controllers
{
    public class StoreController : AppController
    {

        #region Dependencies

        [Inject]
        public IBlockToDBService BlockToDBService { get; set; }

        #endregion Dependencies

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            BlockToDBEditVM blockToDBEdit = BlockToDBService.GetToEdit(id);
            return View(blockToDBEdit);
        }

        [HttpGet]
        public ActionResult GetSchemasToList(DataSourceLoadOptions loadOptions)
        {
            object data = BlockToDBService.GetSchemasToList(loadOptions);
            return CustomJson(data);
        }

        [HttpPost]
        public ActionResult EditSchemaData(BlockToDBEditVM model)
        {
            if (ModelState.IsValid)
            {
                BlockToDBService.Edit(model);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Created, "Edited");
        }

        [HttpDelete]
        public ActionResult DeleteDSchemaData(int id)
        {
            BlockToDBService.Delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.Created, "Deleted");
        }
    }
}