using Microsoft.Owin;
using Models.Startups;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quary.Startup))]
namespace Quary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Authentication.ConfigureAuth(app);
        }
    }
}
