using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Esthetic.Models;

namespace Esthetic.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Categories()
        {
            AdminModel model = new AdminModel();
            model.Categories = ImageCtrler.Instance.GetCategories();
            return View(model);
        }

        public ActionResult Images()
        {
            AdminModel model = new AdminModel();
            model.Categories = ImageCtrler.Instance.GetCategories();
            return View(model);
        }

        [HttpPost]
        public ActionResult CreateCategory(AdminModel model)
        {
            string name = model.NewCategoryName;
            string desc = model.NewCategoryDescription;
            int parent_id = model.CategorySelected;

            ImageCtrler.Instance.CreateCategory(new Category() { Name = name, Description = desc, Parent = new Category() { Id = parent_id } });
            return RedirectToAction("Categories");
        }

        [HttpPost]
        public ActionResult UpdateCategory(AdminModel model, int id)
        {
            string name = model.EditCategoryName;
            string desc = model.EditCategoryDescription;
            int parent_id = model.CategorySelected;

            ImageCtrler.Instance.UpdateCategory(new Category() {Id = id, Name = name, Description = desc, Parent = new Category() { Id = parent_id } });
            return RedirectToAction("Categories");
        }

        public ActionResult DeleteCategory(int id)
        {
            ImageCtrler.Instance.DeleteCategory(id);
            return RedirectToAction("Categories");
        }
    }
}