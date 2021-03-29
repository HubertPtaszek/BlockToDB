using BlockToDB.Application;
using BlockToDB.Dictionaries;
using BlockToDB.Resources.Shared;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    public class UserHelper
    {
        public static AppUserData GetUserData(bool force = false, string userName = "")
        {
            if (HttpContext.Current == null)
                return null;
            if (userName.IsNullOrEmpty())
            {
                userName = HttpContext.Current.User.Identity.Name;
            }
            if (userName.IsNullOrEmpty())
                throw new AuthorizationException(ErrorResource.NoLoginAdded);
            AppUserData userData = null;

            userData = HttpContext.Current.Session?[SessionVariableNames.AppUserData] as AppUserData;
            bool isTheSameUserName = true;
            if (userData == null || !isTheSameUserName || force)
            {
                IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();
                userData = userService.GetFirstUser();
                if (userData == null)
                    throw new AuthorizationException(ErrorResource.NoLoginAdded);
                if (HttpContext.Current.Session != null)
                {
                    HttpContext.Current.Session[SessionVariableNames.AppUserData] = userData;
                }
            }

            return userData;
        }

        public static void RefreshUserData()
        {
            string userName = HttpContext.Current.User.Identity.Name;
            if (userName.IsNullOrEmpty())
                return;
            AppUserData userData = HttpContext.Current.Session[SessionVariableNames.AppUserData] as AppUserData;
            if (userData != null && userData.UserName == userName)
            {
                IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();
                userData = userService.GetFirstUser();
                if (userData == null)
                    throw new AuthorizationException(ErrorResource.NoLoginAdded);
                HttpContext.Current.Session[SessionVariableNames.AppUserData] = userData;
            }
        }

        public static int GetUnknownUserId()
        {
            IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();
            return userService.GetUnknownUserId();
        }

        public static bool IsAdministrator
        {
            get
            {
                AppUserData userData = GetUserData();
                return userData.Roles.Any(x => x == AppRoleType.Administrator);
            }
        }
    }
}