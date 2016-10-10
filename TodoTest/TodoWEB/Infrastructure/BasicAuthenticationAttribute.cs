using System;
using System.Web.Mvc;
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
            _userChecker = new UserChecker(new EntityRepository<User>());
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var req = filterContext.HttpContext.Request;
            var auth = req.Headers["Authorization"];
            if (!String.IsNullOrEmpty(auth))
            {
                var cred = System.Text.Encoding.ASCII.GetString(Convert.FromBase64String(auth.Substring(6))).Split(':');
                var user = new { Name = cred[0], Pass = cred[1] };
                var webUser = _userChecker.IsValid(user.Name, user.Pass);
                if ( webUser != null)
                {
                    if (filterContext.HttpContext.Session != null)
                    {
                        filterContext.HttpContext.Session["WebUser"] = new WebUser()
                        {
                            Login = user.Name,
                            Password = user.Pass,
                            UserId = webUser.UserId
                        };
                    }
                    return;
                }
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", "Basic realm=\"Test\"");
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}