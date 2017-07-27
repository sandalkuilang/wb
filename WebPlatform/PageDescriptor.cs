using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace WebPlatform
{
    public class PageDescriptor
    {
        private HttpRequestBase request;

        public PageDescriptor(HttpRequestBase request)
        {
            this.request = request;
        }

        public string GetBaseUrl()
        {
            return request.ServerVariables["SCRIPT_NAME"];
        }
    }
}
