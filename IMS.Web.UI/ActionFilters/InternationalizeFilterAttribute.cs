using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Compaid.Common.Ioc.TinyIoc;
using IMS.Common.Data.Interfaces;
using System.Web.Mvc;
using System.Web.Routing;

namespace IMS.Web.UI.ActionFilters
{
    public class InternationalizeMVCFilterAttribute : ActionFilterAttribute, IActionFilter
    {
        /// <summary>
        /// Overriding the Action Handler used prior to calling the actual controller.
        /// </summary>
        /// <param name="actionContext">The HttpActionContext object</param>
        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            //Get the principle if there is one
            IUserIdentityProvider principle = TinyIocContainer.Current.Resolve<IUserIdentityProvider>();

            //Only Internationalize if the principle has been set.  If it hasn't been set, just return
            if (principle != null && principle.CurrentUser != null)
            {
                //Loop through the arguments and internationalize 
                foreach (var parameter in actionContext.ActionParameters)
                {
                    if (parameter.Value is IEnumerable<IInternationalize>)
                    {
                        //Loop through each parameter to see if a datetime is there to convert
                        (parameter.Value as IEnumerable<IInternationalize>).ToList().ForEach(row => row.InternationalizeRequest(principle));
                    }
                    else if (parameter.Value is IInternationalize)
                    {
                        (parameter.Value as IInternationalize).InternationalizeRequest(principle);
                    }
                    else if (parameter.Value is IEnumerable<IEnumerable<IInternationalize>>)
                    {
                        //Loop through each parameter in the inner List to see if a datetime is there to convert
                        (parameter.Value as IEnumerable<IEnumerable<IInternationalize>>).ToList().ForEach(row => row.ToList().ForEach(innerRow => innerRow.InternationalizeRequest(principle)));
                    }

                    //Loop throught each property of the parameter and 
                    //internationalize if it has an object that implements the IInternationalize interface or is a List of IInternationalize
                    foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(parameter.Value))
                    {
                        if (property.GetValue(parameter.Value) is IEnumerable<IInternationalize>)
                        {
                            //Loop through each parameter to see if a datetime is there to convert
                            (property.GetValue(parameter.Value) as IEnumerable<IInternationalize>).ToList().ForEach(row => row.InternationalizeRequest(principle));
                        }
                        else if (property.GetValue(parameter.Value) is IInternationalize)
                        {
                            (property.GetValue(parameter.Value) as IInternationalize).InternationalizeRequest(principle);
                        }
                    }
                }
            }
            else
            {
                string actionName = actionContext.ActionDescriptor.ActionName;
                string controllerName = actionContext.ActionDescriptor.ControllerDescriptor.ControllerName;

                if ((actionName != "Logout" || actionName != "Index") && !controllerName.Equals("Login", StringComparison.OrdinalIgnoreCase))
                {
                    actionContext.Result = new RedirectToRouteResult(new RouteValueDictionary(
                   new { action = "Logout", controller = "Login" }));

                }
            }

            base.OnActionExecuting(actionContext);
        }

        /// <summary>
        /// Overriding the Action Handler used after the actual controller logic has been run.
        /// </summary>
        /// <param name="filterContext">The HttpActionExecutedContext object</param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Get the user info
            //Get the principle if there is one
            IUserIdentityProvider principle = TinyIocContainer.Current.Resolve<IUserIdentityProvider>();

            //Only Internationalize if the principle has been set.  If it hasn't been set, just return
            if (principle != null && filterContext.Result != null)
            {
                var objectContent = filterContext.Result as ViewResultBase;
                var ajaxContent = filterContext.Result as JsonResult;

                if (objectContent != null)
                {
                    if (objectContent.Model is IEnumerable<IInternationalize>)
                    {
                        //Loop through each parameter to see if a datetime is there to convert
                        (objectContent.Model as IEnumerable<IInternationalize>).ToList().ForEach(row => row.InternationalizeResponse(principle));
                    }
                    else if (objectContent.Model is IInternationalize)
                    {
                        (objectContent.Model as IInternationalize).InternationalizeResponse(principle);
                    }
                    else if (objectContent.Model is IEnumerable<IEnumerable<IInternationalize>>)
                    {
                        //Loop through each parameter in the inner List to see if a datetime is there to convert
                        (objectContent.Model as IEnumerable<IEnumerable<IInternationalize>>).ToList().ForEach(row => row.ToList().ForEach(innerRow => innerRow.InternationalizeResponse(principle)));
                    }

                    //Loop throught each property of the parameter and 
                    //internationalize if it has an object that implements the IInternationalize interface or is a List of IInternationalize
                    foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(objectContent.Model))
                    {
                        if (property.GetValue(objectContent.Model) is IEnumerable<IInternationalize>)
                        {
                            //Loop through each parameter to see if a datetime is there to convert
                            (property.GetValue(objectContent.Model) as IEnumerable<IInternationalize>).ToList().ForEach(row => row.InternationalizeResponse(principle));
                        }
                        else if (property.GetValue(objectContent.Model) is IInternationalize)
                        {
                            (property.GetValue(objectContent.Model) as IInternationalize).InternationalizeResponse(principle);
                        }
                    }
                }
                else if(ajaxContent != null)
                {

                    if (ajaxContent.Data is IEnumerable<IInternationalize>)
                    {
                        //Loop through each parameter to see if a datetime is there to convert
                        (ajaxContent.Data as IEnumerable<IInternationalize>).ToList().ForEach(row => row.InternationalizeResponse(principle));
                    }
                    else if (ajaxContent.Data is IInternationalize)
                    {
                        (ajaxContent.Data as IInternationalize).InternationalizeResponse(principle);
                    }
                    else if (ajaxContent.Data is IEnumerable<IEnumerable<IInternationalize>>)
                    {
                        //Loop through each parameter in the inner List to see if a datetime is there to convert
                        (ajaxContent.Data as IEnumerable<IEnumerable<IInternationalize>>).ToList().ForEach(row => row.ToList().ForEach(innerRow => innerRow.InternationalizeResponse(principle)));
                    }

                    //Loop throught each property of the parameter and 
                    //internationalize if it has an object that implements the IInternationalize interface or is a List of IInternationalize
                    foreach (PropertyDescriptor property in TypeDescriptor.GetProperties(ajaxContent.Data))
                    {
                        if (property.GetValue(ajaxContent.Data) is IEnumerable<IInternationalize>)
                        {
                            //Loop through each parameter to see if a datetime is there to convert
                            (property.GetValue(ajaxContent.Data) as IEnumerable<IInternationalize>).ToList().ForEach(row => row.InternationalizeResponse(principle));
                        }
                        else if (property.GetValue(ajaxContent.Data) is IInternationalize)
                        {
                            (property.GetValue(ajaxContent.Data) as IInternationalize).InternationalizeResponse(principle);
                        }
                    }
                }
            }

            base.OnActionExecuted(filterContext);
        }
    }
}