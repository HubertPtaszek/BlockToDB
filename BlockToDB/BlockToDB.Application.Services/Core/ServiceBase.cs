using BlockToDB.Application.Abstraction;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockToDB.Infrastructure;
using BlockToDB.Utils;

namespace BlockToDB.Application
{
    public abstract class ServiceBase : IService
    {
        [Inject]
        public MainContext MainContext { get; set; }

        [Inject]
        public DbSession DbSession { get; set; }
    }
}
