using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Database;
using WebPlatform.Credential;
using WebPlatform.Menu;
using System.Web.Mvc;

namespace WebPlatform
{
    public class BaseLandingController : BaseController
    {
        public override void Startup()
        {
            
        }
         
        public virtual ActionResult Index()
        { 
            if (HttpContext.Session.Lookup() != null)
            {
                //HttpContext.Session.Lookup().Register<User>().ImplementedBy(null); 
            }
            this.Startup(); 
            return View();
        }
         
    }
}
