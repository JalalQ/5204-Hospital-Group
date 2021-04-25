using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeraldtonHospV7.Startup))]
namespace GeraldtonHospV7
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
