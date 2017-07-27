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

namespace WebPlatform
{
    public class SecuritySettings
    {
        public string LoginControlller { get; set; }
        public string UnauthorizedControlller { get; set; }
        public bool EnableAuthentication { get; set; }
        public bool SkipAuthorization { get; set; }
        public string UnauthorizedAlert { get; set; }

        public SecuritySettings()
        {
            // default value
            LoginControlller = "Login";
            UnauthorizedControlller = "Unauthorized";
            EnableAuthentication = true;
            SkipAuthorization = false;
            UnauthorizedAlert = "You are not authorized access this page.";
        }
    }
}
