using BlockToDB.Application.Abstraction;
using BlockToDB.Infrastructure;
using Ninject;

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