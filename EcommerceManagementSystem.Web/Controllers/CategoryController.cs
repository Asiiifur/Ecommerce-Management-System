using EcommerceManagementSystem.Manager;
using EcommerceManagementSystem.Models;
using EcommerceManagementSystem.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EcommerceManagementSystem.Web.Controllers
{
   //[Authorize (Roles ="Admin")]
    public class CategoryController : Controller
    {



        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CategoryTable(string search, int? pageNO)
        {
            CategorySearchViewModel model = new CategorySearchViewModel();
            model.SearchTerm = search;

            pageNO = pageNO.HasValue ? pageNO.Value > 0 ? pageNO.Value : 1 : 1;

            var totalRecords = CategoryManager.Instance.GetCategoriesCount(search);

            model.Categories = CategoryManager.Instance.GetCategories(search, pageNO.Value);

            if (model.Categories != null)
            {
                model.Pager = new Pager(model.Categories.Count, pageNO, 15);

                return PartialView("CategoryTable", model);
            }
            else
            {
                return HttpNotFound();
            }
        }


        [HttpGet]
        public ActionResult Create()
        {
            NewCategoryViewModel model = new NewCategoryViewModel();

            return PartialView(model);
        }
        [HttpPost]
        public ActionResult Create(NewCategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newCategory = new Category();
                newCategory.Name = model.Name;
                newCategory.Description = model.Description;
                newCategory.ImageURL = model.ImageURL;
                newCategory.IsFeatured = model.IsFeatured;

                CategoryManager.Instance.SaveCategory(newCategory);

                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }


        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditCategoryViewModel model = new EditCategoryViewModel();

            var category = CategoryManager.Instance.GetCategory(ID);

            model.ID = category.ID;
            model.Name = category.Name;
            model.Description = category.Description;
            model.ImageURL = category.ImageURL;
            model.IsFeatured = category.IsFeatured;

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditCategoryViewModel model)
        {
            var existingCategory = CategoryManager.Instance.GetCategory(model.ID);
            existingCategory.Name = model.Name;
            existingCategory.Description = model.Description;
            existingCategory.ImageURL = model.ImageURL;
            existingCategory.IsFeatured = model.IsFeatured;

            CategoryManager.Instance.UpdateCategory(existingCategory);

            return RedirectToAction("CategoryTable");
        }


        //[HttpGet]
        //public ActionResult Edit(int ID)
        //{
        //    var category = CategoryManager.Instance.GetCategory(ID);
        //    return View(category);
        //}
        //[HttpPost]
        //public ActionResult Edit (Category category)

        //{
        //    CategoryManager.Instance.UpdateCategory(category);
        //    return RedirectToAction("Index");
        //}
        //[HttpGet]
        //public ActionResult Delete(int ID)
        //{
        //    var category = CategoryManager.Instance.GetCategory(ID);
        //    return View(category);
        //}
        //[HttpPost]
        //public ActionResult Delete(Category category)
        //{

        //    CategoryManager.Instance.DeleteCategory(category.ID);
        //    return RedirectToAction("Index");
        //}
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            CategoryManager.Instance.DeleteCategory(ID);

            return RedirectToAction("CategoryTable");
        }
    }
}