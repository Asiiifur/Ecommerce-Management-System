using EcommerceManagementSystem.Manager;
using EcommerceManagementSystem.Web.Code;
using EcommerceManagementSystem.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceManagementSystem.Web.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        
        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNO)
        {
            ShopViewModel model = new ShopViewModel();

            model.FeaturedCategories = CategoryManager.Instance.GetFeaturedCategories();
            model.MaximunPrice = ProductManager.Instance.GetMaximumPrice();
            pageNO = pageNO.HasValue ? pageNO.Value > 0 ? pageNO.Value : 1 : 1;
            model.Products = ProductManager.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNO.Value, 10);
            model.Sortby = sortBy;
            model.CategoryID = categoryID;
            int totalCount = ProductManager.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Pager = new Pager(totalCount, pageNO);

            return View(model);
        }
        public ActionResult FilterProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNO)
        {
            FilterProductsViewModel model = new FilterProductsViewModel();

            model.Products = ProductManager.Instance.SearchProducts(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNO.Value, 10);
            int totalCount = ProductManager.Instance.SearchProductsCount(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            model.Pager = new Pager(totalCount, pageNO);
            return PartialView(model);
        }

        public ActionResult Checkout()
        {
            CheckoutViewModel model = new CheckoutViewModel();

            var CartProductCookie = Request.Cookies["CartProducts"];
            if (CartProductCookie != null)
            {
                //var productIDs = CartProductCookie.Value;
                //var ids = productIDs.Split('-');
                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                model.CartProductIDs = CartProductCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();
                model.CartProducts = ProductManager.Instance.GetProducts(model.CartProductIDs);

            }

            return View(model);
        }
    }
}