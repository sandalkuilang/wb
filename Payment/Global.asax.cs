using Payment.Models.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using WebPlatform;

namespace Payment
{
    public class MvcApplication : WebApplicationStartup
    {
        public override void Startup()
        {
            SessionPool.Instance.Register<IUserAccountRepository>().ImplementedBy<UserAccountRepository>();
            this.Settings.Landing.LoginController = "Home";
        }

        public override void Initialize()
        {
              
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        } 
    }
}
