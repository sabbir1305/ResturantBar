using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ResturantBar.Startup))]
namespace ResturantBar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
