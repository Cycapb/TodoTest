﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Paginator.Abstract;
using TodoDAL.Models;
using TodoWEB.Abstract;
using TodoWEB.Infrastructure;
using TodoWEB.Models;

namespace TodoWEB.Controllers
{
    [BasicAuthentication]
    public class HomeController : Controller
    {
        private readonly ITodoManager _todoManager;
        private readonly IPageCreator _pageCreator;
        private readonly int _itemsPerPage = 7;

        public HomeController(ITodoManager todoManager, IPageCreator pageCreator)
        {
            _todoManager = todoManager;
            _pageCreator = pageCreator;
        }

        public async Task<ActionResult> Index(WebUser user)
        {
            if (user == null)
            {
                user = (WebUser)HttpContext.Session?["WebUser"];
            }
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
                TotalItems = _todoManager.GetList(user.UserId).Count(),
                ItemsPerPage = _itemsPerPage
            };
            ViewBag.PageCreator = _pageCreator;
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
        public async Task<ActionResult> Delete(int id, int page)
        {
            await _todoManager.DeleteAsync(id);
            return RedirectToAction("List", new {page = page});
        }

        [HttpPost]
        public async Task<ActionResult> Complete(int id, int page)
        {
            await _todoManager.CompleteAsync(id);
            return RedirectToAction("List", new {page = page});
        }

        public async Task<ActionResult> Edit(int id, int page = 1)
        {
            var item = await _todoManager.GetItemAsync(id);
            if (item == null){ return RedirectToAction("List"); }
            var model = new EditViewModel()
            {
                Description = item.Description,
                DtEnd = item.CompletionDate,
                Id = item.TodoId
            };
            ViewBag.Page = page;
            return PartialView(model);
        }

        [HttpPost]
        public async Task<ActionResult> Edit(EditViewModel model, int page)
        {
            if (ModelState.IsValid)
            {
                var todo = await _todoManager.GetItemAsync(model.Id);
                todo.CompletionDate = model.DtEnd;
                todo.Description = model.Description;
                await _todoManager.UpdateAsync(todo);
                return RedirectToAction("List", new {page = page});
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
            ViewBag.PagingInfo = new PagingInfo {CurrentPage = 1};
            return PartialView("_TodoItems",items);
        }
    }
}