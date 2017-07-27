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
    public class LandingPageSettings
    {
        public string HomeController { get; set; }
        public string LandingController { get; set; }
        public string MaintenanceController { get; set; }

        public string LoginController { get; set; }
        public string UnauthorizedControlller { get; set; }
       
        public string UnauthorizedAlert { get; set; }

        public LandingPageSettings()
        {
            HomeController = "Home";
            LandingController = "Home";
            MaintenanceController = "Maintenance";
            LoginController = "Account/Login";
            UnauthorizedControlller = "Unauthorized";
            UnauthorizedAlert = "You are not authorized access this page.";
        }
    }
}
