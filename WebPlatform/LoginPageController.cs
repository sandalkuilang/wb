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
using WebPlatform.Credential;
using WebPlatform.Menu;
using System.Web.Mvc;
using Common.Container;
using Common.Database;
using Crypto;
using WebPlatform.Cryptography;

namespace WebPlatform
{
    public class LoginPageController : BaseController
    { 
        private IUserAuthentication userAuth;

        public LoginPageController() 
            : base("Login")
        {
            userAuth = SessionPool.Instance.Resolve<IUserAuthentication>(); 
        }
          
        public bool LoginUser(string username) 
        { 
            return userAuth.Login(username) != null; 
        }

        public bool LoginUser(string username, string password)
        {
            return userAuth.Login(username, password) != null;
        }
         
        public bool Logout()
        {
            userAuth.Logout();
            return true;
        }
 
        public override void Startup()
        {
            
        }
    }
}
