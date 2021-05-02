using BlockToDB.Application;
using BlockToDB.Utils;
using Ninject;
using System.Net;
using System.Web.Mvc;

namespace BlockToDB.Controllers
{
    [Authorize]
    public class SchemasController : AppController
    {
        #region Dependencies

        [Inject]
        public IBlockToDBService BlockToDBService { get; set; }

        #endregion Dependencies

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetSchemasToList(DataSourceLoadOptions loadOptions)
        {
            object data = BlockToDBService.GetSchemasToList(loadOptions);
            return CustomJson(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddSchemaData(BlockToDBAddVM model)
        {
            if (ModelState.IsValid)
            {
                BlockToDBService.Add(model);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Created, "Added");
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult EditSchemaData(BlockToDBEditVM model)
        {
            if (ModelState.IsValid)
            {
                BlockToDBService.Edit(model);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Created, "Edited");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDSchemaData(int id)
        {
            BlockToDBService.Delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.Created, "Deleted");
        }

        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            BlockToDBEditVM blockToDBEdit = BlockToDBService.GetToEdit(id);
            return View(blockToDBEdit);
        }
    }
}