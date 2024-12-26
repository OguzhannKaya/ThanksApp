using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Controllers.Bases;
using BLL.Services;
using BLL.Models;
using BLL.Services.Bases;
using BLL.DAL;
using Microsoft.AspNetCore.Authorization;

// Generated from Custom Template.

namespace MVC.Controllers
{
    [Authorize]
    public class ThanksController : MvcController
    {
        // Service injections:
        private readonly IService<Thanks,ThanksModel> _thanksService;
        private readonly IService<Category, CategoryModel> _categoryService;
        private readonly IService<User, UserModel> _userService;
        private readonly IService<Tag, TagModel> _tagService;

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //private readonly IManyToManyRecordService _ManyToManyRecordService;

        public ThanksController(
            IService<Thanks, ThanksModel> thanksService
            , IService<Category, CategoryModel> categoryService
            , IService<User, UserModel> userService
            , IService<Tag, TagModel> tagService

        /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
        //, IManyToManyRecordService ManyToManyRecordService
        )
        {
            _thanksService = thanksService;
            _categoryService = categoryService;
            _userService = userService;
            _tagService = tagService;

            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            //_ManyToManyRecordService = ManyToManyRecordService;
        }

        // GET: Thanks
        [AllowAnonymous]
        public IActionResult Index()
        {
            // Get collection service logic:
            var list = _thanksService.Query().ToList();
            return View(list);
        }

        // GET: Thanks/Details/5
        public IActionResult Details(int id)
        {
            // Get item service logic:
            var item = _thanksService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        protected void SetViewData()
        {
            // Related items service logic to set ViewData (Record.Id and Name parameters may need to be changed in the SelectList constructor according to the model):
            ViewData["CategoryId"] = new SelectList(_categoryService.Query().ToList(), "Record.Id", "Name");
            ViewData["UserId"] = new SelectList(_userService.Query().ToList(), "Record.Id", "UserName");
            
            /* Can be uncommented and used for many to many relationships. ManyToManyRecord may be replaced with the related entiy name in the controller and views. */
            ViewBag.TagIds = new MultiSelectList(_tagService.Query().ToList(), "Record.Id", "Name");
        }

        // GET: Thanks/Create
        public IActionResult Create()
        {
            SetViewData();
            return View();
        }

        // POST: Thanks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ThanksModel thanks)
        {
            if (ModelState.IsValid)
            {
                // Insert item service logic:
                var result = _thanksService.Create(thanks.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = thanks.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(thanks);
        }

        // GET: Thanks/Edit/5
        public IActionResult Edit(int id)
        {
            // Get item to edit service logic:
            var item = _thanksService.Query().SingleOrDefault(q => q.Record.Id == id);
            SetViewData();
            return View(item);
        }

        // POST: Thanks/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ThanksModel thanks)
        {
            if (ModelState.IsValid)
            {
                // Update item service logic:
                var result = _thanksService.Update(thanks.Record);
                if (result.IsSuccessful)
                {
                    TempData["Message"] = result.Message;
                    return RedirectToAction(nameof(Details), new { id = thanks.Record.Id });
                }
                ModelState.AddModelError("", result.Message);
            }
            SetViewData();
            return View(thanks);
        }

        // GET: Thanks/Delete/5
        public IActionResult Delete(int id)
        {
            // Get item to delete service logic:
            var item = _thanksService.Query().SingleOrDefault(q => q.Record.Id == id);
            return View(item);
        }

        // POST: Thanks/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // Delete item service logic:
            var result = _thanksService.Delete(id);
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Index));
        }
	}
}
