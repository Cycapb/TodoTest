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
        private readonly int _itemsPerPage = 7;

        public HomeController(ITodoManager todoManager)
        {
            _todoManager = todoManager;
        }

        public async Task<ActionResult> Index(WebUser user)
        {
            var items = (await _todoManager.GetListAsync(user.UserId)).ToList();
            return View(items);
        }

        public ActionResult List(WebUser user, int page = 1)
        {
            var items =  _todoManager.GetList(user.UserId)
                .Where(x => x.UserId == user.UserId)
                .Skip((page - 1)*_itemsPerPage)
                .Take(_itemsPerPage)
                .ToList();
            ViewBag.PagingInfo = new PagingInfo()
            {
                CurrentPage = page,
                TotalItems = _todoManager.GetList(user.UserId).ToList().Count,
                ItemsPerPage = _itemsPerPage
            };
            return PartialView("_TodoList", items);
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
            model.Complete = false;
            if (ModelState.IsValid)
            {
                var todo = new Todo()
                {
                    Description = model.Description,
                    CompletionDate = model.DtEnd,
                    UserId = model.UserId
                };
                await _todoManager.CreateAsync(todo);
                return RedirectToAction("List");
            }
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            await _todoManager.DeleteAsync(id);
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<ActionResult> Complete(int id)
        {
            await _todoManager.CompleteAsync(id);
            return RedirectToAction("List");
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _todoManager.GetItemAsync(id);
            if (item == null){ return RedirectToAction("List"); }
            var model = new EditViewModel()
            {
                Description = item.Description,
                DtEnd = item.CompletionDate,
                Id = item.TodoId
            };
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var todo = await _todoManager.GetItemAsync(model.Id);
                todo.CompletionDate = model.DtEnd;
                todo.Description = model.Description;
                await _todoManager.UpdateAsync(todo);
                return RedirectToAction("List");
            }
            return PartialView(model);
        }

        public async Task<ActionResult> Search(WebUser user, string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                return RedirectToAction("List");
            }
            var items = (await _todoManager.GetListAsync(user.UserId))
                .Where(x => x.Description.ToLower().Contains(query.ToLower()))
                .ToList();
            return PartialView("_TodoItems",items);
        }
    }
}