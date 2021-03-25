using BlockToDB.Application;
using BlockToDB.Data;
using Ninject;
using Ninject.Extensions.Interception.Infrastructure.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockToDB.Infrastructure
{
    internal class BlockToDBBindings
    {
        internal static void Load(IKernel kernel)
        {
            kernel.Bind<IBlockToDBService>().To<BlockToDBService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<BlockToDBService>().To<BlockToDBService>().InMainContextScope();

            kernel.Bind<BlockToDBConverter>().ToSelf().InMainContextScope();

            kernel.Bind<ICodeRepository>().To<CodeRepository>().InMainContextScope();
        }
    }
}
