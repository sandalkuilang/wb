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
using System.Web.Mvc;
using System.IO;

namespace WebPlatform
{
    public static class HtmlExtensions
    {
                  
        public static PlatformExtensions Platform(this HtmlHelper helper)
        {
            if (SessionPool.Instance.Resolve<PlatformExtensions>() == null)
            {
                PlatformExtensions extensions = new PlatformExtensions();
                extensions.HtmlHelper = helper;
                SessionPool.Instance.Register<PlatformExtensions>().ImplementedBy(extensions);
            }
            return SessionPool.Instance.Resolve<PlatformExtensions>();
            
        }
         
    }
}