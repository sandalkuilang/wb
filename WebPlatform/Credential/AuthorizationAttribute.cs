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
using System.Web.Routing;

namespace WebPlatform.Credential
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);

            PageController controller = (PageController)filterContext.Controller;
            controller.Authentication.Authenticate(controller.ControllerContext.RequestContext);

            if (!controller.Authentication.IsValid)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                {
                    action = controller.PageSettings.IndexPage,
                    controller = ApplicationSettings.Instance.Landing.LoginController
                }));
            }
            else if (!controller.Authentication.IsAuthorized && !ApplicationSettings.Instance.AuthenticationSetting.SkipAuthorization)
            {
                if (string.IsNullOrEmpty(ApplicationSettings.Instance.Landing.UnauthorizedControlller))
                {
                    ContentResult result = new ContentResult();
                    result.Content = new MvcHtmlString(ApplicationSettings.Instance.Landing.UnauthorizedAlert).ToHtmlString();
                    filterContext.Result = result;
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
                    {
                        action = controller.PageSettings.IndexPage,
                        controller = ApplicationSettings.Instance.Landing.UnauthorizedControlller
                    }));
                }
            } 
        } 

    }
}
