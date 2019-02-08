using Compaid.Common.Ioc.TinyIoc;
using IMS.Common.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;

namespace IMS.Web.UI
{
    public class IMSAuthenticationAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public string Roles { get; set; }
        
        public void OnAuthentication(AuthenticationContext filterContext)
        {
            var user = filterContext.HttpContext.User;
            if ((user == null) || (user.Identity.IsAuthenticated == false))
            {
                filterContext.Result = new RedirectToRouteResult("Default",
                new System.Web.Routing.RouteValueDictionary{
                        {"controller", "Login"},
                        {"action", "Index"},
                        {"returnUrl", filterContext.HttpContext.Request.RawUrl}
                    });
            }
            else
            {
                IUserIdentityProvider provider = TinyIocContainer.Current.Resolve<IUserIdentityProvider>();

                bool isValid = provider.IsInRole(this.Roles);

                if (isValid == false)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Login", action = "AccessDenied" }));
                }
            }
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            
        }
    }
}