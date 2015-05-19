using Microsoft.Owin;
using Twitter.Web;

[assembly: OwinStartup(typeof(Startup))]

namespace Twitter.Web
{
    using Owin;

    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}