using System;
using System.Web.Mvc;
using Ninject;
using TodoDAL.Abstract;
using TodoDAL.Concrete;
using TodoDAL.Models;
using TodoWEB.Abstract;
using TodoWEB.Concrete;
using TodoWEB.Models;

namespace TodoWEB.Infrastructure
{
    public class BasicAuthenticationAttribute:ActionFilterAttribute
    {
        private readonly IUserChecker _userChecker;

        public BasicAuthenticationAttribute()
        {
            var k = System.Web.Mvc.DependencyResolver.Current.GetService(typeof(Infrastructure.NinjectDependencyResolver));
            _userChecker = ((Infrastructure.NinjectDependencyResolver)k).Kernel.TryGet<IUserChecker>();
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!String.IsNullOrEmpty(auth))
            {
                var cred = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                if (_userChecker.IsValid(user.Name, user.Pass))
                {
                    if (filterContext.HttpContext.Session != null)
                    {
                        filterContext.HttpContext.Session["WebUser"] = new WebUser()
                        {
                            Login = user.Name,
                            Password = user.Pass
                        };
                    }
                    return;
                }
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", "Basic realm=\"Test\"");
            filterContext.Result = new HttpUnauthorizedResult();
        }

        private IKernel CreateNinjectKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IUserChecker>().To<UserChecker>();
            kernel.Bind<IRepository<User>>().To<EntityRepository<User>>();
            return kernel;
        }
    }
}