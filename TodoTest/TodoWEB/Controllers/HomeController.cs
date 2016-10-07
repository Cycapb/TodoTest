using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using TodoDAL.Models;
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

        [HttpPost]
        public async Task<ActionResult> Add(WebUser user, AddViewModel model)
        {
            model.UserId = user.UserId;
            if (ModelState.IsValid)
            {
                var todo = new Todo()
                {
                    Description = model.Description,
                    CompletionDate = model.DtEnd,
                    UserId = model.UserId
                };
                await _todoManager.CreateAsync(todo);
            }
            return PartialView(model);
        }
    }
}