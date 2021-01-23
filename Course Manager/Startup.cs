using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Course_Manager.Startup))]
namespace Course_Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
