using System.Linq;
using System.Threading.Tasks;
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

        public async Task<ActionResult> Index(WebUser user)
        {
            var items = (await _todoManager.GetListAsync(user.UserId)).ToList();
            return View(items);
        }

        public ActionResult Add()
        {
            var model = new AddViewModel();
            return PartialView(model);
        }
    }
}