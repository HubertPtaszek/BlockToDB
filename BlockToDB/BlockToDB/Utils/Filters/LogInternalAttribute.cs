using Microsoft.AspNet.Identity;
using System;
using System.Security.Principal;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class LogInternalAttribute : FilterAttribute, IActionFilter
    {
        public string Message { get; set; }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User as IPrincipal;
            var accountId = user == null ? null : user.Identity.GetUserId();
            var userName = user == null ? null : user.Identity.GetUserName();
            var message = "";

            var controller = filterContext.Controller as Controller;
        }
    }
}