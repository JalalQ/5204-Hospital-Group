using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Simran_hospital.Startup))]
namespace Simran_hospital
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
