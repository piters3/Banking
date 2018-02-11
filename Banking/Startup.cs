using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Banking.Startup))]
namespace Banking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
