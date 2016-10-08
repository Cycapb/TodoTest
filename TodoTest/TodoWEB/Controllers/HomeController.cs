﻿using System.Linq;
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

        public async Task<ActionResult> List(WebUser user)
        {
            var items = (await _todoManager.GetListAsync(user.UserId)).ToList();
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
    }
}