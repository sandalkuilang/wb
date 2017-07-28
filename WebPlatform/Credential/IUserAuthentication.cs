using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPlatform.Credential
{
    public interface IUserAuthentication
    {
        User Login(string username);
        User Login(string username, string password);
        void Logout();
    }
}
