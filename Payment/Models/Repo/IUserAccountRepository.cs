using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Payment.Models.Repo
{
    public interface IUserAccountRepository
    {
        void Register(dynamic param);
        bool Login(dynamic param);
        List<UserAccount> GetUsersList(dynamic param);
    }
}