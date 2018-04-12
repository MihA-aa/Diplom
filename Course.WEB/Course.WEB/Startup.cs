using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Course.WEB.Startup))]
namespace Course.WEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
