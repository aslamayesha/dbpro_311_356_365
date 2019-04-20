using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(inventory_store.Startup))]
namespace inventory_store
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
