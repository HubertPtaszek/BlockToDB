using BlockToDB.Application;
using BlockToDB.Resources.Shared;
using System.Linq;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    [Authorize]
    public class AdministratorAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public AdministratorAuthorizationAttribute()
        {
        }

        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                filterContext.Result = new HttpUnauthorizedResult();
                return;
            }
            AppUserData appUserData = UserHelper.GetUserData();
            if (!appUserData.Roles.Any(x => x == BlockToDB.Dictionaries.AppRoleType.Administrator))
            {
                throw new AuthorizationException(ErrorResource.AccessDenied);
            }
        }
    }
}