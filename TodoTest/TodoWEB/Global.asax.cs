using System.Web.Mvc;
using System.Web.Routing;
using TodoWEB.Infrastructure;
using TodoWEB.Models;

namespace TodoWEB
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(WebUser),new UserModelBinder());
        }
    }
}
