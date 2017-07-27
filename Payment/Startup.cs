using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Payment.Startup))]
namespace Payment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
