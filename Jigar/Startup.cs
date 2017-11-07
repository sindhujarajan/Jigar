using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jigar.Startup))]
namespace Jigar
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
