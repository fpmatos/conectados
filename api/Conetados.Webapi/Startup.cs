using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Conetados.Webapi.Startup))]
namespace Conetados.Webapi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
