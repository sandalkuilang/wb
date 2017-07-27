using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Krokot.Container;
using System.Web;
using System.Web.SessionState;
using System.Web.Mvc;
using WebPlatform.Credential;
using Krokot.Database;
using WebPlatform.Menu;

namespace WebPlatform
{
    public static class HttpSessionStateExtensions
    {
        public static IContainer Lookup(this HttpSessionStateBase session)
        {
            if (session != null)
            {
                return (IContainer)session[GlobalConst.SessionPoolName];
            }
            return null;
        }
         
    }
}
