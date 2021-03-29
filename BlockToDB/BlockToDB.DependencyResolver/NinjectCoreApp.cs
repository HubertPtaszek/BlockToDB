using BlockToDB.Application;
using BlockToDB.Domain;
using BlockToDB.EntityFramework;
using Hangfire;
using Microsoft.AspNet.Identity.EntityFramework;
using Ninject.Modules;
using Ninject.Syntax;
using Ninject.Web.Common;
using System;
using System.Linq;

namespace BlockToDB.Infrastructure
{
    public class NinjectCoreApp : NinjectModule
    {
        public override void Load()
        {
            Bind<MainContext>().ToSelf().InNamedOrBackgroundJobScope(context =>
            context.Kernel.Components.GetAll<INinjectHttpApplicationPlugin>().Select(c => c.GetRequestScope(context)).FirstOrDefault(s => s != null));

            Bind<TransactionInterceptor>().ToSelf().InTransientScope();
            Bind<Func<IdentityDbContext<AppIdentityUser>>>().ToMethod(context => MainDatabaseContext.Create).InSingletonScope();
            Bind<MainDatabaseContext>().ToSelf().InMainContextScope();
            Bind<IdentityDbContext<AppIdentityUser>>().ToMethod(context => (MainDatabaseContext)context.Kernel.GetService(typeof(MainDatabaseContext))).InMainContextScope();
            Bind<DbSession>().ToSelf().InMainContextScope();

            CoreBindings.Load(Kernel);
            MembershipBindings.Load(Kernel);
            BlockToDBBindings.Load(Kernel);
        }
    }

    public static class MainContextScope
    {
        public static IBindingNamedWithOrOnSyntax<T> InMainContextScope<T>(this IBindingInSyntax<T> syntax)
        {
            return syntax.InScope(x => ((MainContext)x.Kernel.GetService(typeof(MainContext))));
        }
    }
}