using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using BlockToDB.Models;
using BlockToDB.Utils;
using Ninject;
using BlockToDB.Application;
using System.Net;

namespace BlockToDB.Controllers
{
    [Authorize]
    public class SchemasController : AppController
    {
        #region Dependencies

        [Inject]
        public IBlockToDBService BlockToDBService { get; set; }
        #endregion Dependencies

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetDepartmentData(DataSourceLoadOptions loadOptions)
        {
            object data = BlockToDBService.GetCaseToList(loadOptions);
            return CustomJson(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDepartmentData(BlockToDBAddVM model)
        {
            if (ModelState.IsValid)
            {
                BlockToDBService.Add(model);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Created, "Added");
        }

        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult EditDepartmentData(BlockToDBEditVM model)
        {
            if (ModelState.IsValid)
            {
                BlockToDBService.Edit(model);
            }
            return new HttpStatusCodeResult(HttpStatusCode.Created, "Edited");
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteDepartmentData(int id)
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