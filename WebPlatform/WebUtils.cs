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
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using Krokot.Container;
using System.Web.SessionState;

namespace WebPlatform
{
    public static class WebUtils
    {
           
        public static void SetContextValue<T>(object key, T value)
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                context.Items[key] = value;
            }
        }

        public static T GetContextValue<T>(object key)
        {
            HttpContext current = HttpContext.Current;
            if ((current != null) && current.Items.Contains(key))
            {
                return (T)current.Items[key];
            }
            return default(T);
        }

        public static string GetLogonUser(HttpRequestBase request)
        {
            string logonUser = request.ServerVariables[GlobalConst.LOGON_USER_NAME]; 
            return ParseUserLogon(logonUser);
        }

        public static string ParseUserLogon(string name)
        {
            string logonUser = name;
            int indexOfDelimeter = logonUser.IndexOf(@"\");
            if (indexOfDelimeter > 0)
            {
                return logonUser.Substring(indexOfDelimeter + 1);
            }
            return string.Empty;
        }

    }
}
