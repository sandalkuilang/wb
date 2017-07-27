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
using WebPlatform.Credential;
using System.Web;
using System.Web.Mvc;

namespace WebPlatform
{
    public class PageExtensions
    { 
        public PageSettings PageSettings
        {
            get
            {
                HttpSessionStateBase session = new HttpSessionStateWrapper(HttpContext.Current.Session);
                return session.Lookup().Resolve<PageSettings>();
            }
        }

        
        public HtmlHelper HtmlHelper { get; set; }
        
    }
}
