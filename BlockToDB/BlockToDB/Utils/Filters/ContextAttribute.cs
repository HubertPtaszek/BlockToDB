using BlockToDB.Application;
using BlockToDB.Infrastructure;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    public class ContextAttribute : FilterAttribute, IAuthorizationFilter
    {
        public ContextAttribute()
        {
        }

        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            AppUserData userData = UserHelper.GetUserData();
            MainContext context = DependencyResolver.Current.GetService<MainContext>();
            context.PersonId = userData.Id;
        }
    }
}