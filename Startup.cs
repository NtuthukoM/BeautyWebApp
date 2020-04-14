using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BeautyWebApp.Startup))]
namespace BeautyWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
