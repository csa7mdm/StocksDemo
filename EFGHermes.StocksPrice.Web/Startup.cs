using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EFGHermes.StocksPrice.Web.Startup))]
namespace EFGHermes.StocksPrice.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
