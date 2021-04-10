using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hospital_prj.Startup))]
namespace hospital_prj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
