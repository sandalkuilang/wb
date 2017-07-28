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

namespace WebPlatform
{
    public class LoginPageController : BaseController
    {
        private IDbManager dbManager;
        private IUserProvider userProvider;
        public LoginPageController() 
            : base("Login")
        {
            dbManager = SessionPool.Instance.Resolve<IDbManager>(); 
            userProvider = SessionPool.Instance.Resolve<IUserProvider>();
        }
          
        public bool LoginUser(string username) 
        {
            if (ApplicationSettings.Instance.AuthenticationSetting.EnableAuthentication)
            { 
                string defaultDbName = dbManager.ConnectionDescriptor.Where(x => x.IsDefault).Select(c => c.Name).SingleOrDefault();
                if (userProvider == null)
                    userProvider = new DatabaseUserProvider(dbManager);
                                
                User user = userProvider.GetUser(username);
                return Authenticate(user);
            }
            else
            {
                return true;
            }
        }

        public bool LoginUser(string username, string password)
        {
            if (ApplicationSettings.Instance.AuthenticationSetting.EnableAuthentication)
            {
                string defaultDbName = dbManager.ConnectionDescriptor.Where(x => x.IsDefault).Select(c => c.Name).SingleOrDefault();
                if (userProvider == null)
                    userProvider = new DatabaseUserProvider(dbManager);

                User user = userProvider.GetUser(username, password);
                return Authenticate(user);
            }
            else
            {
                return true;
            }
        }

        public bool Authenticate(User user)
        {
            if (user != null)
            {
                user.Role = userProvider.GetAuthorization(user);

                Session.Lookup().Register<User>().ImplementedBy(user);
                Session.Lookup().Register<IUserProvider>().ImplementedBy(userProvider);

                IMenuProvider<MenuCollection> menuProvider = Session.Lookup().Resolve<IMenuProvider<MenuCollection>>();

                if (menuProvider == null)
                {
                    menuProvider = new DatabaseMenuProvider(dbManager);
                    menuProvider.User = user;
                    menuProvider.Build();
                    Session.Lookup().Register<IMenuProvider<MenuCollection>>().ImplementedBy(menuProvider);
                }
                else
                {
                    menuProvider.User = user;
                    menuProvider.Build();
                }
                return user != null;
            }
            return false;
        }

        public bool Logout()
        {
            Session.Lookup().Unregister<User>();
            Session.Lookup().Unregister<PageSettings>();
            Session.Lookup().Unregister<MenuExtensions>();
            Session.Lookup().Unregister<IUserProvider>();
            Session.Lookup().Unregister<IMenuProvider<MenuCollection>>(); 
            Session.Lookup().Unregister<ApplicationSettings>();
            Session.Lookup().Unregister<PlatformExtensions>();
            return true;
        }
 
        public override void Startup()
        {
            
        }
    }
}
