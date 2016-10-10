using System.Web.Mvc;
using TodoWEB.Models;
using IModelBinder = System.Web.Mvc.IModelBinder;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;

namespace TodoWEB.Infrastructure
{
    public class UserModelBinder:IModelBinder
    {
        private const string SessionKey = "WebUser";

        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            WebUser user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = (WebUser)controllerContext.HttpContext.Session[SessionKey];
            }
            return user;
        }
    }
}