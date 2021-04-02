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
            //check roles
        }
    }
}