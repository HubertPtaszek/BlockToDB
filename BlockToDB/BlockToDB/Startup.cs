using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlockToDB.Startup))]
namespace BlockToDB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
