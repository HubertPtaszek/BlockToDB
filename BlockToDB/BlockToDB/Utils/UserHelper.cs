using BlockToDB.Application;
using Microsoft.AspNet.Identity;
using System.Web;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    public class UserHelper
    {
        public static AppUserData GetUserData()
        {
            string webUserId = HttpContext.Current.User.Identity.GetUserId();
            AppUserData userData = HttpContext.Current.Session[SessionVariableNames.AppUserData] as AppUserData;
            if (userData == null || userData.AppIdentityUserId != webUserId)
            {
                IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();
                if (webUserId.IsNullOrEmpty())
                {
                    userData = new AppUserData();
                    userData.Id = GetUnknownUserId();
                }
                else
                {
                    userData = userService.GetUserDataByWebUserId(webUserId);
                }
                HttpContext.Current.Session[SessionVariableNames.AppUserData] = userData;
            }
            return userData;
        }

        public static int GetUnknownUserId()
        {
            IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();
            return userService.GetUnknownUserId();
        }
    }
}