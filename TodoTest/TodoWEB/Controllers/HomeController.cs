using System.Web.Mvc;
using TodoWEB.Abstract;
using TodoWEB.Models;

namespace TodoWEB.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ITodoManager _todoManager;

        public HomeController(ITodoManager todoManager)
        {
            _todoManager = todoManager;
        }

        public ActionResult Index(WebUser user)
        {
            return View();
        }
    }
}