using Microsoft.Owin;
using Models.Startups;
using Owin;
using Quary.New;

[assembly: OwinStartup(typeof(Startup))]
namespace Quary.New
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            Authentication.ConfigureAuth(app);
        }
    }
}
