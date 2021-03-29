using BlockToDB.Application;
using BlockToDB.Data;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace BlockToDB.Infrastructure
{
    internal class MembershipBindings
    {
        internal static void Load(IKernel kernel)
        {
            kernel.Bind<IAppUserService>().To<AppUserService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<AppUserService>().To<AppUserService>().InMainContextScope();
            kernel.Bind<IAppUserRepository>().To<AppUserRepository>().InMainContextScope();
            kernel.Bind<AppUserConverter>().ToSelf().InMainContextScope();
        }
    }
}