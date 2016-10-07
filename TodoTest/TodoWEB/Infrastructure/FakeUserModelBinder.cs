using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using TodoWEB.Models;
using IModelBinder = System.Web.Mvc.IModelBinder;
using ModelBindingContext = System.Web.Mvc.ModelBindingContext;

namespace TodoWEB.Infrastructure
{
    public class FakeUserModelBinder:IModelBinder
    {
        private const string SessionKey = "WebUser";
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            WebUser user = null;
            if (controllerContext.HttpContext.Session != null)
            {
                user = (WebUser)controllerContext.HttpContext.Session[SessionKey];
            }
            if (user == null)
            {
                user = new WebUser()
                {
                    Login = "Test",
                    Password = "Test",
                    UserId = 1
                };
                if (controllerContext.HttpContext.Session != null)
                {
                    controllerContext.HttpContext.Session["WebUser"] = user;
                }
            }
            return user;
        }
    }
}