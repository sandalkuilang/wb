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
using System.Web.Routing;
using System.Diagnostics;
using WebPlatform.Menu;
using System.Web;
using Common.Database;

namespace WebPlatform.Credential
{
    public class RequestAuthentication
    {
        private string screenId;
        private bool isValid;
        private bool isAuthorized; 

        public RequestAuthentication(string screenId)
        {
            this.screenId = screenId;
        }
 
        public void Authenticate(RequestContext requestContext)
        {
            if (ApplicationSettings.Instance.Security.EnableAuthentication && ApplicationSettings.Instance.Landing.LandingController != screenId)
            {
                User user = requestContext.HttpContext.Session.Lookup().Resolve<User>();
                if (user != null)
                {
                    this.isValid = true;
                    IMenuProvider<MenuCollection> menuProvider = requestContext.HttpContext.Session.Lookup().Resolve<IMenuProvider<MenuCollection>>();
                    if (ApplicationSettings.Instance.Menu.Enabled)
                    {
                        menuProvider.Build();
                    }
                    if (!string.IsNullOrEmpty(screenId))
                    {

                        MenuCollection menuCollection = menuProvider.GetHierarchyMenuData();
                        if (menuCollection != null)
                        {
                            foreach (HierarchyMenuData child in menuCollection)
                            {
                                if (child.Contains(screenId))
                                {
                                    this.isAuthorized = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                this.isValid = true;
                this.isAuthorized = true;
            } 
        }

        public bool IsAuthorized
        {
            get
            {
                return isAuthorized;
            }
        }

        public bool IsValid
        {
            get
            {
                return isValid;
            }
        }
    }
}
