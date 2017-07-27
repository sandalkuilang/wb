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
using System.Web.Mvc;

namespace WebPlatform
{
    public class TechnicalExtensions
    {

        public HtmlHelper HtmlHelper { get; set; }
        
        public string ApplicationName
        {
            get
            {
                return ApplicationSettings.Instance.TechnicalInformation.ApplicationName;
            }
        }
         
        public string CompanyLink
        {
            get
            {
                return ApplicationSettings.Instance.TechnicalInformation.CompanyLink;
            }
        }

        public string CompanyName
        {
            get
            {
                return ApplicationSettings.Instance.TechnicalInformation.CompanyName;
            }
        }

        public string DepartmentName
        {
            get
            {
                return ApplicationSettings.Instance.TechnicalInformation.DepartmentName;
            }
        }

        public string DepartmentContact
        {
            get
            {
                return ApplicationSettings.Instance.TechnicalInformation.DepartmentContact;
            }
        }
         
        public string TechnicalContactName
        {
            get
            {
                return ApplicationSettings.Instance.TechnicalInformation.TechnicalContactName;
            }
        }

        public string TechnicalContactLink
        {
            get
            {
                return ApplicationSettings.Instance.TechnicalInformation.TechnicalContactLink;
            }
        }
         
        public string Home
        {
            get
            {
                return ApplicationSettings.Instance.TechnicalInformation.Home;
            }
        }
    }
}
