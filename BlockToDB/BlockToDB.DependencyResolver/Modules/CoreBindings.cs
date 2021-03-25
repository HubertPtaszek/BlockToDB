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
    internal class CoreBindings
    {
        internal static void Load(IKernel kernel)
        {
            kernel.Bind<IAppSettingsService>().To<AppSettingsService>().InMainContextScope().Intercept().With<TransactionInterceptor>();
            kernel.Bind<AppSettingsService>().To<AppSettingsService>().InMainContextScope();
            kernel.Bind<IAppSettingsRepository>().To<AppSettingsRepository>().InMainContextScope();
        }
    }
}
