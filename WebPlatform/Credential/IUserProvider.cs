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
using System.Web;

namespace WebPlatform.Credential
{
    public interface IUserProvider
    { 
        User GetUser(string username);
        User GetUser(string username, string password);
        Role GetAuthorization(User user);
    }
}
