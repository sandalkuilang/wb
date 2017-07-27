﻿/*
    <author>yudha.hyp@gmail.com</author>
    <summary>
        Copyright (c) yudha, All Right Reserved.
    </summary> 
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebPlatform.Configuration;

namespace WebPlatform
{
    public class ApplicationSettings
    {
        private static readonly ApplicationSettings instance = new ApplicationSettings();

        public static ApplicationSettings Instance
        {
            get
            { 
                return instance;
            }
        }

        public UISettings UI { get; set; }

        public LandingPageSettings Landing { get; set; }

        public MenuSettings Menu { get; set; }
        
        public SecuritySettings  Security{ get; set; }

        public EnvironmentSettings Environment { get; private set; }
        
        public EmailSettings Email{ get; private set; }

        public TechnicalInformationSettings TechnicalInformation { get; private set; }

        internal WebPlatform.Configuration.ConnectionStringSettings Connection { get; private set; }

        public ApplicationSettings()
        {
            Menu = new MenuSettings();
            Security = new SecuritySettings();
            Landing = new LandingPageSettings();
            System.Configuration.Configuration config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");

            Environment = (WebPlatform.Configuration.EnvironmentSettings)config.GetSection("environmentSettings");
            TechnicalInformation = (WebPlatform.Configuration.TechnicalInformationSettings)config.GetSection("technicalInformationSettings");
            Email = (WebPlatform.Configuration.EmailSettings)config.GetSection("emailSettings");
            Connection = (WebPlatform.Configuration.ConnectionStringSettings)config.GetSection("connectionStringCollection");
            
            UI = new UISettings();
        }

    }
}