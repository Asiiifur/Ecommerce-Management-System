﻿using EcommerceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EcommerceManagementSystem.Web.Models.ViewModels
{
    public class CheckoutViewModel
    {
        public List<Product> CartProducts { get; set; }
        public List<int> CartProductIDs { get; set; }
    }
    public class ShopViewModel
    {
        public int MaximunPrice { get; set; }
        public List<Product> Products { get; set; }
        public List<Category> FeaturedCategories { get; set; }
        public int? Sortby { get; set; }
        public int? CategoryID { get; set; }
        public Pager Pager { get; set; }
    }
    public class FilterProductsViewModel
    {
        public List<Product> Products { get; set; }
        public Pager Pager { get; set; }
        public int? Sortby { get; set; }
        public int? CategoryID { get; set; }
    }
}