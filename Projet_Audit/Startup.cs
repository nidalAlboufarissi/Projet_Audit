using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Projet_Audit.Startup))]
namespace Projet_Audit
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
