using Common.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPlatform;

namespace Payment.Models.Repo
{
    public class UserAccountRepository : BaseCommandRespository, IUserAccountRepository
    {
        public bool Login(dynamic param)
        {
            this.Database.Open();
            List<string> result = this.Database.Query<string>("Login", param);
            this.Database.Close();
            if (result.Any())
                return true;

            return false;
        }

        public void Register(dynamic param)
        {
            this.Database.Open();
            List<string> result = this.Database.Query<string>("Register", param);
            this.Database.Close(); 
        }
    }
}