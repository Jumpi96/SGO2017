using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SGO.Startup))]
namespace SGO
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
