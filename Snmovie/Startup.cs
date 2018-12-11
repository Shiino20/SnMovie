using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Snmovie.Startup))]
namespace Snmovie
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
