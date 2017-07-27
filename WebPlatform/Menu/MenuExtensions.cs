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
using System.Web.UI;
using System.Web;
using WebPlatform.Credential;
using System.IO;

namespace WebPlatform.Menu
{
    public class MenuExtensions : BaseRazorHtmlExtensions
    { 

        public MenuExtensions()
            : base(false)
        {
            
        }

        public IMenuProvider<MenuCollection> Provider { get; set; }
         
        public User User
        {
            get { return Provider.User; }
        }

        public StringBuilder CreateMenu()
        {
            StringBuilder html = new StringBuilder();
            MenuCollection menus = Provider.GetHierarchyMenuData();
            if (menus != null)
            { 
                
                html.AppendLine("<ul>");
                foreach (HierarchyMenuData menu in menus)
                {
                    IterateNode(menu, ref html);
                } 
                html.AppendLine(@"</ul>");
            } 
            return html;
        }
         
        private void IterateNode(HierarchyMenuData current, ref StringBuilder html)
        {
            if (current.HasChildren)
            {
                html.AppendLine(string.Format("<li class='has-sub'><a href=\"{0}\">{1}</a>", current.Url == "" ? "#" : current.Url, current.Description));
                html.AppendLine("<ul>");
            }
            else
            {
                if ((current.Url.Length == 1) && (current.Url == @"/"))
                    html.AppendLine(string.Format("<li class='has-sub'><a href=\"{0}\">{1}</a>", GetBaseUrl, current.Description));
                else
                {
                    if (string.IsNullOrEmpty(GetPathInfoUrl))
                        html.AppendLine(string.Format("<li class='has-sub'><a href=\"{0}\">{1}</a>", "/" + current.Url, current.Description));
                    else
                        html.AppendLine(string.Format("<li class='has-sub'><a href=\"{0}\">{1}</a>", "/" + GetPathInfoUrl + "/" + current.Url, current.Description));
                }
                    
            }

            foreach (HierarchyMenuData item in current.Children)
            {
                IterateNode(item, ref html);
            }
            if (current.HasChildren)
                html.AppendLine("</ul></li>");
        }

        protected override StringBuilder OnExecute()
        {
            return CreateMenu();
        }
    }
}
