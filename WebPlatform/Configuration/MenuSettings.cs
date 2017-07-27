/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebPlatform.Configuration
{
    public class MenuSettings
    {
        public bool Enabled { get; set; }

        public MenuSettings()
        {
            Enabled = true;
        }
    }
}