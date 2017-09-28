using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ShopBay.Startup))]
namespace ShopBay
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
