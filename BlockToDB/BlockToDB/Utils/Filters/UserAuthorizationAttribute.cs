using BlockToDB.Application;
using BlockToDB.Infrastructure;
using System.Web.Mvc;

namespace BlockToDB.Utils
{
    [Authorize]
    public class UserAuthorizationAttribute : FilterAttribute, IAuthorizationFilter
    {
        public UserAuthorizationAttribute()
        {
        }

        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            MainContext context = DependencyResolver.Current.GetService<MainContext>();
            AppUserData userData = new AppUserData()
            {
                Id = 3
            };
            context.PersonId = userData.Id;
        }
    }
}