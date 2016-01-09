using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartAdminMvc.Models
{
    public class SmartAdminMvcAuthorize : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return true;
            //return (httpContext.Session["IsAuthenticated"] as bool?) == true;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext authorizationContext)
        {
            var urlHelper = new UrlHelper(authorizationContext.RequestContext);

            authorizationContext.Result = new RedirectResult(urlHelper.Action("Login", "Account", new { returnUrl = authorizationContext.HttpContext.Server.UrlEncode(authorizationContext.HttpContext.Request.Url.PathAndQuery) }));
        }
    }
}
