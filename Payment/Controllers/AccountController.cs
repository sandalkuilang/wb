using Payment.Models;
using Payment.Models.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Payment.Controllers
{
    public class AccountController : Controller
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
                repo.Register(new { }); 
            }
        }
         
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(UserAccount account)
        { 
            IUserAccountRepository repo = new UserAccountRepository();
            repo.Login(new { }); 
        }
    }
}