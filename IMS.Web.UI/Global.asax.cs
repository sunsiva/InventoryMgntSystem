using Compaid.Common.Ioc.TinyIoc;
using Compaid.Common.Serialization;
using Compaid.Web.Ioc;
using IMS.Business.Security;
using IMS.Common.Data.Interfaces;
using IMS.Web.UI;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace IMS.Web.UI
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            GlobalConfiguration.Configuration.Filters.Add(new IMSExceptionAttribute());
            ControllerBuilder.Current.SetControllerFactory(new ControllerFactory());
            Bootstrap.Instance.Register();
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;

            var authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            if (authTicket == null) return;

            var serializationProvider = TinyIocContainer.Current.Resolve<ISerializationProvider>();
            var userData = serializationProvider.DeserializeToObject<IMSPrincipalData>(authTicket.UserData);
            HttpContext.Current.User = new IMSPrincipal(authTicket.Name, userData);

            var userProvider = TinyIocContainer.Current.Resolve<IUserIdentityProvider>();
            userProvider.LoadCurrentUser();
        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }
    }
}
