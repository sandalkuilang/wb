using Payment.Models;
using Payment.Models.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPlatform;

namespace Payment.Controllers
{
    public class AccountController : LoginPageController
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                IUserAccountRepository repo = new UserAccountRepository();
                repo.Register(new {  }); 
            }
            ModelState.Clear();
            return View();
        }
         
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount account)
        {  
            if (!base.Login(account.UserName, account.Password)) 
            {
                ModelState.AddModelError("", "Username or Password is wrong");
            }
            return View();
        }
         
    }
}