using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BD_Nube.Startup))]
namespace BD_Nube
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
