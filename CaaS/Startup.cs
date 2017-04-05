using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CaaS.Startup))]
namespace CaaS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
