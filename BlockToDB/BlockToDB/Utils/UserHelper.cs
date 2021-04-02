using BlockToDB.Application;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    public class UserHelper
    {
        public static int GetUnknownUserId()
        {
            IAppUserService userService = DependencyResolver.Current.GetService<IAppUserService>();
            return userService.GetUnknownUserId();
        }
    }
}