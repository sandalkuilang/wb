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

        public LandingPageSettings()
        {
            HomeController = "Home";
            MaintenanceController = "Maintenance"; 
        }
    }
}
