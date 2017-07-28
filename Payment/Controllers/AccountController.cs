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

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                IUserAccountRepository repo = SessionPool.Instance.Resolve<IUserAccountRepository>();
                repo.Register(new
                {
                    Username = account.UserName,
                    Firstname = account.FirstName,
                    Lastname = account.LastName,
                    Password = account.Password,
                    Email = account.Email,
                    IsActive = 1,
                    CreatedBy = "System",
                    CreatedDate = DateTime.Now
                }); 
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
            if (!base.LoginUser(account.UserName, account.Password)) 
            {
                ModelState.AddModelError("", "User name or Password is wrong");
                return View();
            }
            else
            { 
                return RedirectToRoute("Default");
            }
        }
         
    }
}