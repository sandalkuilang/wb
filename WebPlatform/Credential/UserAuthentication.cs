using Common.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPlatform.Cryptography;
using WebPlatform.Menu;

namespace WebPlatform.Credential
{
    public class UserAuthentication : IUserAuthentication
    { 
        private IUserProvider userProvider;

        public UserAuthentication()
        { 
            userProvider = SessionPool.Instance.Resolve<IUserProvider>();
        }
         
        public User Login(string username, string password)
        {
            if (ApplicationSettings.Instance.AuthenticationSetting.EnableAuthentication)
            {
                User user;
                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                    user = userProvider.GetUser(username, password);
                else
                    user = userProvider.GetUser(username);

                Authenticate(user);
                return user;
            }
            else 
                return null; 
        }

        public User Login(string username)
        {
            return Login(username, null);
        }
         
        private bool Authenticate(User user)
        {
            if (user != null)
            {
                user.Role = userProvider.GetAuthorization(user);

                SessionPool.Instance.Register<User>().ImplementedBy(user);
                SessionPool.Instance.Register<IUserProvider>().ImplementedBy(userProvider);

                IMenuProvider<MenuCollection> menuProvider = SessionPool.Instance.Resolve<IMenuProvider<MenuCollection>>();

                if (menuProvider == null)
                {
                    menuProvider = new DatabaseMenuProvider(SessionPool.Instance.Resolve<IDbManager>());
                    menuProvider.User = user;
                    menuProvider.Build();
                    SessionPool.Instance.Register<IMenuProvider<MenuCollection>>().ImplementedBy(menuProvider);
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

        public void Logout()
        {
            SessionPool.Instance.Unregister<User>();
            SessionPool.Instance.Unregister<PageSettings>();
            SessionPool.Instance.Unregister<MenuExtensions>();
            SessionPool.Instance.Unregister<IUserProvider>();
            SessionPool.Instance.Unregister<IMenuProvider<MenuCollection>>();
            SessionPool.Instance.Unregister<ApplicationSettings>();
            SessionPool.Instance.Unregister<PlatformExtensions>();
            SessionPool.Instance.Unregister<Crypto.IEncryptionAgent>();
            SessionPool.Instance.Unregister<IEncryptionFileAgent>();
        }
    }
}
