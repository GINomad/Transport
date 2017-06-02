using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Transport.WEB.Startup))]
namespace Transport.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
