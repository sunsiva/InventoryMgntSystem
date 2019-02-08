using Compaid.Common.Ioc.TinyIoc;
using Compaid.Common.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http.Filters;
using System.Web.Mvc;

namespace IMS.Web.UI
{
    public class IMSExceptionAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            IExceptionProvider provider = TinyIocContainer.Current.Resolve<IExceptionProvider>();
            provider.Error(actionExecutedContext.Exception);
        }
    }
}