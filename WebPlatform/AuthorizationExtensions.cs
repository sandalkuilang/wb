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

namespace WebPlatform
{
    public class AuthorizationExtensions : BaseRazorHtmlExtensions
    { 
        
        public bool IsAuthorizedFeature(string functionId, string feature)
        {
            return GetAuthorization(functionId, feature) != null;
        }

        public AuthorizationUIPolicy GetAuthorizationUIPolicy(string functionId, string feature)
        {
            return GetAuthorization(functionId, feature).AuthorizationUIPolicy;
        }

        public bool IsVisible(string functionId, string feature)
        {
            AuthorizationFeatureQualifier auth = GetAuthorization(functionId, feature); 

            if (auth.AuthorizationUIPolicy == AuthorizationUIPolicy.Visible)
                return true;
            else if (auth.AuthorizationUIPolicy == AuthorizationUIPolicy.Hidden)
                return false;
            return false;
        }

        public bool IsReadonly(string functionId, string feature)
        {
            AuthorizationFeatureQualifier auth = GetAuthorization(functionId, feature); 

            if (auth.AuthorizationUIPolicy == AuthorizationUIPolicy.Readonly)
                return true;
            else
                return false;
        }

        public AuthorizationFeatureQualifier GetAuthorization(string functionId, string feature)
        {
            User user = this.HtmlHelper.Platform().User;
            PageSettings pageSettings = this.HtmlHelper.Platform().Page.PageSettings;
            if (user != null)
            {
                if (user.Role != null)
                {
                    foreach (AuthorizationFunction function in user.Role.Functions)
                    {
                        foreach (AuthorizationFeatureQualifier feat in function.Features)
                        {
                            if (feat.Qualifier.Equals(feature) && feat.Key.Equals(functionId) && function.Name.Equals(pageSettings.ScreenId))
                            {
                                return feat;
                            }
                        }
                    }
                }
            }
            return new AuthorizationFeatureQualifier() { AuthorizationUIPolicy = AuthorizationUIPolicy.Visible, Id = "Invalid", Key = "Invalid", Qualifier = "Invalid" };
        }

        public bool IsAuthorizedFunction(string functionId)
        {
            User user = this.HtmlHelper.Platform().User; 
            if (user != null)
            {
                if (user.Role != null)
                {
                    foreach (AuthorizationFunction function in user.Role.Functions)
                    {
                        if (function.Name.Equals(functionId))
                        {
                            return true;
                        } 
                    }
                }
            }
            return false;
        }
         
        protected override StringBuilder OnExecute()
        {
            throw new NotImplementedException();
        }
    }
}
