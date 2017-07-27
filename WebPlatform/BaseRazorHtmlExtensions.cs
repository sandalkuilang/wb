using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.IO;
using System.Web.UI;
using System.Web;

namespace WebPlatform
{
    public abstract class BaseRazorHtmlExtensions
    { 
        public bool GeneratedOnce { get; set; }
        private StringBuilder generatedMenu;

        public BaseRazorHtmlExtensions()
            : this(true)
        { 
        }

        public BaseRazorHtmlExtensions(bool generatedOnce)
        {
            GeneratedOnce = generatedOnce;
        }

        public string Hostname
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
            }
        }

        public string GetBaseUrl
        {
            get
            {
                return HttpContext.Current.Request.ServerVariables["SCRIPT_NAME"] + "/";
            }
        }
         
        public MvcHtmlString GetHtml()
        {
            if (GeneratedOnce && generatedMenu == null)
                generatedMenu = OnExecute();
            else
                generatedMenu = OnExecute();

            return new MvcHtmlString(generatedMenu.ToString());
        }

        public string GetPathInfoUrl
        {
            get
            { 
                return System.IO.Path.GetFileName(HttpContext.Current.Request.ServerVariables["APPL_MD_PATH"]); 
            }
        }

        public string GetHttp
        {
            get
            {
                if (HttpContext.Current.Request.ServerVariables["HTTPS"] == "off")
                {
                    return "http://";
                }
                return "https://";
            }
        }

        public void Render()
        { 
            HtmlTextWriter Writer = new HtmlTextWriter(HtmlHelper.ViewContext.Writer);
            if (!GeneratedOnce && generatedMenu == null)
                generatedMenu = OnExecute();

            Writer.WriteLine(generatedMenu.ToString()); 
        }

        public virtual HtmlHelper HtmlHelper { get; set; }

        protected abstract StringBuilder OnExecute(); 
    }
}
