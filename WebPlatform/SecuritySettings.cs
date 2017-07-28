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
    public class AuthenticationSettings
    { 
        public bool EnableAuthentication { get; set; }
        public bool SkipAuthorization { get; set; } 

        public AuthenticationSettings()
        {
            // default value
            EnableAuthentication = true;
            SkipAuthorization = false; 
        }
    }
}
