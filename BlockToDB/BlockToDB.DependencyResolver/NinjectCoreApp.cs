using Ninject.Modules;
using Ninject.Syntax;
using Ninject.Web.Common;
using System.Linq;
using BlockToDB.EntityFramework;
using BlockToDB.Application;
using Hangfire;

namespace BlockToDB.Infrastructure
{
    public class NinjectCoreApp : NinjectModule
    {
        public override void Load()
        {
            Bind<MainContext>().ToSelf().InNamedOrBackgroundJobScope(context =>
            context.Kernel.Components.GetAll<INinjectHttpApplicationPlugin>().Select(c => c.GetRequestScope(context)).FirstOrDefault(s => s != null) /*?? InCustomParentScope(context)*/);

            Bind<TransactionInterceptor>().ToSelf().InTransientScope();
            Bind<MainDatabaseContext>().ToSelf().InMainContextScope();
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
