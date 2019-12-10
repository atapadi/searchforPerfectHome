using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchForPerfectHome.Startup))]
namespace SearchForPerfectHome
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
