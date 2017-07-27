using System.Web;
using Krokot.Container;

namespace WebPlatform
{
    public class SessionPool
    {
        public static IContainer Instance
        {
            get
            {
                if (HttpContext.Current != null)
                {
                    HttpSessionStateBase session = new HttpSessionStateWrapper(HttpContext.Current.Session);
                    if (session[GlobalConst.SessionPoolName] == null)
                    {
                        session[GlobalConst.SessionPoolName] = new Container(GlobalConst.ContainerKey);
                    }
                    return (IContainer)session[GlobalConst.SessionPoolName];
                }
                else
                {
                    return null;
                } 
            }
        }
    }
}
