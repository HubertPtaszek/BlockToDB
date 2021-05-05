using BlockToDB.Application;
using BlockToDB.Data;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;

namespace BlockToDB.Infrastructure
{
    internal class BlockToDBBindings
    {
        internal static void Load(IKernel kernel)
        {
            kernel.Bind<IBlockToDBService>().To<BlockToDBService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<BlockToDBService>().To<BlockToDBService>().InMainContextScope();
            kernel.Bind<IDatabaseSchemaRepository>().To<DatabaseSchemaRepository>().InMainContextScope();

            kernel.Bind<BlockToDBConverter>().ToSelf().InMainContextScope();
        }
    }
}