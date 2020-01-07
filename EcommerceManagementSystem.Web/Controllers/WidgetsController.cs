using EcommerceManagementSystem.Manager;
using EcommerceManagementSystem.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceManagementSystem.Web.Controllers
{
    public class WidgetsController : Controller
    {
        // GET: Widgets
        public ActionResult Products(bool isLetestProducts, int? CategoryID)
        {
            ProducWidgetViewModel model = new ProducWidgetViewModel();
            model.IsLatestProducts = isLetestProducts;
            if (isLetestProducts)
            {
                model.Products = ProductManager.Instance.GetLetestProducts(4);
            }
            else if (CategoryID.HasValue && CategoryID.Value > 0)
            {
                model.Products = ProductManager.Instance.GetProductsByCategory(CategoryID.Value, 4);
            }
            else
            {
                model.Products = ProductManager.Instance.GetProducts(1, 8);
            }

            return PartialView(model);
        }
    }
}