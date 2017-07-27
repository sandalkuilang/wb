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
using System.Configuration;

namespace WebPlatform.Configuration
{
    public class EnvironmentSettings : ConfigurationSection
    {

        [ConfigurationProperty("developmentContext")]
        public string DevelopmentContext
        {
            get
            {
                return (string)this["developmentContext"];
            }
            set
            {
                this["developmentContext"] = value;
            }
        }

        [ConfigurationProperty("personCacheSecond")]
        public string PersonCacheSecond
        {
            get
            {
                return (string)this["personCacheSecond"];
            }
            set
            {
                this["personCacheSecond"] = value;
            }
        }

        [ConfigurationProperty("applicationNameUrl")]
        public string ApplicationNameUrl
        {
            get
            {
                return (string)this["applicationNameUrl"];
            }
            set
            {
                this["applicationNameUrl"] = value;
            }
        }

        [ConfigurationProperty("applicationId")]
        public string ApplicationId
        {
            get
            {
                return (string)this["applicationId"];
            }
            set
            {
                this["applicationId"] = value;
            }
        }
        
    }
}