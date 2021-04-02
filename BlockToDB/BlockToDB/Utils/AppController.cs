using BlockToDB.Infrastructure;
using Ninject;
using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    [ExceptionHandler]
    [NoCache]
    [AllowAnonymous]
    public abstract class AppController : Controller
    {
        [Inject]
        public MainContext MainContext { get; set; }

        private static object _lockObject = new object();

        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl-PL");
            Thread.CurrentThread.CurrentCulture = new CultureInfo("pl-PL");
            base.OnAuthorization(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);
        }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }

        [NonAction]
        protected string RenderViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                try
                {
                    viewResult.View.Render(viewContext, sw);
                }
                catch (Exception ex)
                {
                }
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        [NonAction]
        protected CustomJsonResult CustomJson(object data, JsonRequestBehavior behavior = JsonRequestBehavior.DenyGet, string dateTimeFormat = DateTimeFormats.IsoDateTimeFormat)
        {
            return new CustomJsonResult() { Data = data, JsonRequestBehavior = behavior, DateTimeFormat = dateTimeFormat };
        }
    }
}