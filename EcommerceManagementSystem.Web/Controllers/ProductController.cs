using EcommerceManagementSystem.Manager;
using EcommerceManagementSystem.Models;
using EcommerceManagementSystem.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceManagementSystem.Web.Controllers
{
    public class ProductController : Controller
    {
        //ProductManager productManager = new ProductManager();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNO)
        {
            var pageSize = ConfigurationsManager.Instance.PageSize();
            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;

            pageNO = pageNO.HasValue ? pageNO.Value > 0 ? pageNO.Value : 1 : 1;

            var totalRecords = ProductManager.Instance.GetProductsCount(search);
            model.Products = ProductManager.Instance.GetProducts(search, pageNO.Value, pageSize);

            model.Pager = new Pager(totalRecords, pageNO, pageSize);


            return PartialView(model);
        }
        [HttpGet]
        public ActionResult Create()
        {
            NewProductViewModel model = new NewProductViewModel();

            model.AvailableCategories = CategoryManager.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Create(NewProductViewModel model)
        {
            var newProduct = new Product();
            newProduct.Name = model.Name;
            newProduct.Description = model.Description;
            newProduct.Price = model.Price;
            newProduct.Category = CategoryManager.Instance.GetCategory(model.CategoryID);
            newProduct.ImageURL = model.ImageURL;

            ProductManager.Instance.SaveProduct(newProduct);
            return RedirectToAction("ProductTable");
        }
        //[HttpGet]
        //public ActionResult Create()
        //{

        //    var categories = CategoryManager.Instance.GetCategories();
        //    return  PartialView(categories);
        //}
        //[HttpPost]
        //public ActionResult Create(NewCategoryViewModel model)
        //{
        //    //productManager.SaveProduct(product);

        //    var newProduct = new Product();
        //    newProduct.Name = model.Name;
        //    newProduct.Description = model.Description;
        //    newProduct.Price = model.Price;
        //    newProduct.Category = CategoryManager.Instance.GetCategory(model.CategoryID);
        //    ProductManager.Instance.SaveProduct(newProduct);

        //    return RedirectToAction("ProductTable");
        //}



        [HttpGet]
        public ActionResult Edit(int ID)
        {
            EditProductViewModel model = new EditProductViewModel();

            var product = ProductManager.Instance.GetProduct(ID);

            model.ID = product.ID;
            model.Name = product.Name;
            model.Description = product.Description;
            model.Price = product.Price;
            model.CategoryID = product.Category != null ? product.Category.ID : 0;
            model.ImageURL = product.ImageURL;

            model.AvailableCategories = CategoryManager.Instance.GetAllCategories();

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Edit(EditProductViewModel model)
        {
            var existingProduct = ProductManager.Instance.GetProduct(model.ID);
            existingProduct.Name = model.Name;
            existingProduct.Description = model.Description;
            existingProduct.Price = model.Price;

            existingProduct.Category = null; //mark it null. Because the referncy key is changed below
            existingProduct.CategoryID = model.CategoryID;

            //dont update imageURL if its empty
            if (!string.IsNullOrEmpty(model.ImageURL))
            {
                existingProduct.ImageURL = model.ImageURL;
            }

            ProductManager.Instance.UpdateProduct(existingProduct);

            return RedirectToAction("ProductTable");
        }
        //[HttpGet]
        //public ActionResult Edit(int ID)
        //{
        //    var product = ProductManager.Instance.GetProduct(ID);
        //    return PartialView(product);
        //}
        //[HttpPost]
        //public ActionResult Edit(Product product)
        //{
        //    ProductManager.Instance.UpdateProduct(product);

        //    return RedirectToAction("ProductTable");
        //}
        [HttpPost]
        public ActionResult Delete(int ID)
        {
            ProductManager.Instance.DeleteProduct(ID);

            return RedirectToAction("ProductTable");
        }

        [HttpGet]
        public ActionResult Details(int ID)
        {
            ProductViewModel model = new ProductViewModel();

            model.Product = ProductManager.Instance.GetProduct(ID);

            if (model.Product == null) return HttpNotFound();

            return View(model);
        }
    }
}