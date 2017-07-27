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
using System.Web.Mvc;
using WebPlatform.Menu;
using System.Web;

namespace WebPlatform
{
    public class PlatformExtensions
    {    
        private HtmlHelper helper;
 
        public PlatformExtensions()
        {
            Menu = new MenuExtensions();
            Menu.Provider = SessionPool.Instance.Resolve<IMenuProvider<MenuCollection>>();
            Page = new PageExtensions();
            Authorization = new AuthorizationExtensions();
            Information = new TechnicalExtensions();
            UI = ApplicationSettings.Instance.UI;
        }

        public void Build()
        {
            
        }

        public User User
        {
            get
            {
                return SessionPool.Instance.Resolve<User>();
            }
        }

        public HtmlHelper HtmlHelper
        {
            get
            {
                return this.helper;
            }
            set
            {
                this.helper = value;
                this.Page.HtmlHelper = value;
                this.Information.HtmlHelper = value;
                this.Menu.HtmlHelper = value;
                this.Authorization.HtmlHelper = value; 
            }
        }

        public UISettings UI { get; set; }

        public PageExtensions Page { get; set; }

        public TechnicalExtensions Information { get; set; }

        public MenuExtensions Menu { get; set; }
        
        public AuthorizationExtensions Authorization { get; set; }
    }
}
