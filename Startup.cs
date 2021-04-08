using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GeraldtonHospital_v1.Startup))]
namespace GeraldtonHospital_v1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
