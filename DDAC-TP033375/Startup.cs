using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DDAC_TP033375.Startup))]
namespace DDAC_TP033375
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
