using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Esthetic.Startup))]
namespace Esthetic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
