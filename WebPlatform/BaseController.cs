/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using WebPlatform.Credential;
using System.Web;
using System.Web.Security;
using WebPlatform.Menu;
using Common.Database;

namespace WebPlatform
{
    
    public abstract class BaseController : Controller
    {
        public PageDescriptor PageDescriptor { get; private set; }
        public PageSettings PageSettings { get; set; }
        public RequestAuthentication Authentication { get; private set; }
        private string title;

        public BaseController() : this("")
        {
             
        }  

        public BaseController(string title)
        { 
            this.title = title;  
        }
         
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            //// initiate every incoming request, enable if load balancer on
            if (requestContext.HttpContext.Session.Lookup() == null)
            {
                ContainerFactory containerFactory = new ContainerFactory(System.Web.HttpContext.Current);
                containerFactory.Unregister(SessionPool.Instance);
                containerFactory.Register(SessionPool.Instance); 
            } 
            PageSettings = new PageSettings(this.GetType(), this.title);
            Authentication = new RequestAuthentication(PageSettings.ScreenId); 
            requestContext.HttpContext.Session.Lookup().Register<PageSettings>().ImplementedBy(this.PageSettings);
            base.Initialize(requestContext); 
        }
         
        public bool IsAuthorized
        {
            get
            {
                return Session.Lookup().Resolve<User>() != null;
            }
        }

        public User AuthorizedUser
        {
            get
            {
                return Session.Lookup().Resolve<User>();
            }
        }

        public abstract void Startup();
    }
}
