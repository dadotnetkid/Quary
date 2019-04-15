using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Quary.Startup))]
namespace Quary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
