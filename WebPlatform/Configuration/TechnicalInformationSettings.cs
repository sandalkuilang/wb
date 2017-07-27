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
    public class TechnicalInformationSettings : ConfigurationSection
    {

        [ConfigurationProperty("applicationName")]
        public string ApplicationName
        {
            get
            {
                return (string)this["applicationName"];
            }
            set
            {
                this["applicationName"] = value;
            }
        }

        [ConfigurationProperty("companyName")]
        public string CompanyName
        {
            get
            {
                return (string)this["companyName"];
            }
            set
            {
                this["companyName"] = value;
            }
        }

        [ConfigurationProperty("companyLink")]
        public string CompanyLink
        {
            get
            {
                return (string)this["companyLink"];
            }
            set
            {
                this["companyLink"] = value;
            }
        }

        [ConfigurationProperty("departmentName")]
        public string DepartmentName
        {
            get
            {
                return (string)this["departmentName"];
            }
            set
            {
                this["departmentName"] = value;
            }
        }

        [ConfigurationProperty("departmentContact")]
        public string DepartmentContact
        {
            get
            {
                return (string)this["departmentContact"];
            }
            set
            {
                this["departmentContact"] = value;
            }
        }

        [ConfigurationProperty("technicalContactName")]
        public string TechnicalContactName
        {
            get
            {
                return (string)this["technicalContactName"];
            }
            set
            {
                this["technicalContactName"] = value;
            }
        }

        [ConfigurationProperty("technicalContactLink")]
        public string TechnicalContactLink
        {
            get
            {
                return (string)this["technicalContactLink"];
            }
            set
            {
                this["technicalContactLink"] = value;
            }
        }
          
        [ConfigurationProperty("home")]
        public string Home
        {
            get
            {
                return (string)this["home"];
            }
            set
            {
                this["home"] = value;
            }
        }

    }
}