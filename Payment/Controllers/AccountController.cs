using Payment.Models;
using Payment.Models.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebPlatform;

namespace Payment.Controllers
{ 
    public class AccountController : LoginPageController
    {
        // GET: Account
        public ActionResult Index()
        {
            IUserAccountRepository repo = SessionPool.Instance.Resolve<IUserAccountRepository>();            
            return View(repo.GetUsersList(null));
        }

    }
}