/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/

using System.Web;

namespace WebPlatform
{
    public abstract class WebApplicationStartup : System.Web.HttpApplication
    {
        private ContainerFactory containerFactory;

        /// <summary>
        /// Global configuration of platform, other sessions also have access to it.
        /// </summary>
        public ApplicationSettings Settings
        {
            get
            {
                return ApplicationSettings.Instance;
            }
        }
         
        protected void Session_Start()
        {
            containerFactory = new ContainerFactory(HttpContext.Current);
            containerFactory.Register(SessionPool.Instance);

            this.Startup();
        }

        protected void Session_End()
        { 
            if (SessionPool.Instance != null)
                containerFactory.Unregister(SessionPool.Instance);
        }

        protected virtual void Application_Start()
        {
            //RouteCollectionExtensions.IgnoreRoute(RouteTable.Routes, "{resource}.axd/{*pathInfo}");
            //RouteCollectionExtensions.MapRoute(RouteTable.Routes, "_default_route_", "{controller}/{action}/{id}", 
            //        new 
            //        { 
            //            controller = "Home", 
            //            action = "Index", 
            //            id = UrlParameter.Optional 
            //        });

            this.Initialize();
        }
         
        public abstract void Startup(); 
        public abstract void Initialize();

    }
}