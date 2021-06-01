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

        [HttpGet]
        public ActionResult GetData(DataSourceLoadOptions loadOptions)
        {
            object data = BlockToDBService.GetSchemasToList(loadOptions);
            return CustomJson(data);
        }

        [HttpDelete]
        public ActionResult DeleteSchemaData(int id)
        {
            BlockToDBService.Delete(id);
            return new HttpStatusCodeResult(HttpStatusCode.Created, "Deleted");
        }
    }
}