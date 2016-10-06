using System;
using System.Web.Mvc;
using TodoWEB.Abstract;

namespace TodoWEB.Infrastructure
{
    public class BasicAuthenticationAttribute:ActionFilterAttribute
    {
        private readonly IUserChecker _userChecker;
        public string BasicRealm { get; set; }

        public BasicAuthenticationAttribute(IUserChecker userChecker)
        {
            _userChecker = userChecker;
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
                    return;
                }
            }
            filterContext.HttpContext.Response.AddHeader("WWW-Authenticate", $"Basic realm=\"{BasicRealm ?? "Test"}\"");
            filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}