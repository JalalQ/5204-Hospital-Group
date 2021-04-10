using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(hospitalPrj.Startup))]
namespace hospitalPrj
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
