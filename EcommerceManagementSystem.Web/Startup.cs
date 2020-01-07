using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EcommerceManagementSystem.Web.Startup))]
namespace EcommerceManagementSystem.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
