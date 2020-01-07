﻿using EcommerceManagementSystem.Database;
using EcommerceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace EcommerceManagementSystem.Manager
{
    public class ProductManager
    {
        #region Singleton
        public static ProductManager Instance
        {
            get
            {
                if (instance == null) instance = new ProductManager();
                return instance;
            }
        }
        private static ProductManager instance { get; set; }

        private ProductManager()
        {

        }
        #endregion

        public List<Product> SearchProducts(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int pageNO, int pageSize)
        {
            using (var context = new EMSDBContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {

                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;


                    }
                }
                return products.Skip((pageNO - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        public int SearchProductsCount(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            using (var context = new EMSDBContext())
            {
                var products = context.Products.ToList();

                if (categoryID.HasValue)
                {
                    products = products.Where(x => x.Category.ID == categoryID.Value).ToList();
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
                }
                if (minimumPrice.HasValue)
                {
                    products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
                }
                if (maximumPrice.HasValue)
                {
                    products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
                }
                if (sortBy.HasValue)
                {
                    switch (sortBy.Value)
                    {

                        case 2:
                            products = products.OrderByDescending(x => x.ID).ToList();
                            break;
                        case 3:
                            products = products.OrderBy(x => x.Price).ToList();
                            break;
                        default:
                            products = products.OrderByDescending(x => x.Price).ToList();
                            break;


                    }
                }
                return products.Count;
            }
        }

        public int GetMaximumPrice()
        {
            using (var context = new EMSDBContext())
            {

                return (int)(context.Products.Max(x => x.Price));

            }
        }

        public Product GetProduct(int ID)
        {
            using (var context = new EMSDBContext())
            {
                return context.Products.Where(x => x.ID == ID).Include(x => x.Category).FirstOrDefault();

            }
        }

        public List<Product> GetProducts(List<int> IDs)
        {
            using (var context = new EMSDBContext())
            {
                return context.Products.Where(product => IDs.Contains(product.ID)).ToList();
            }
        }

        public List<Product> GetProducts(int pageNO)
        {
            int pageSize = 15; //int.Parse(ConfigurationsManager.Instance.GetConfig("ListingPageSize").Value);
            using (var context = new EMSDBContext())
            {

                return context.Products.OrderBy(x => x.ID).Skip((pageNO - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();
                //return context.Products.Include(x => x.Category).ToList();

            }
        }

        public List<Product> GetProducts(int pageNO, int pageSize)
        {

            using (var context = new EMSDBContext())
            {

                return context.Products.OrderByDescending(x => x.ID).Skip((pageNO - 1) * pageSize).Take(pageSize).Include(x => x.Category).ToList();


            }
        }
        public List<Product> GetProductsByCategory(int CategoryID, int pageSize)
        {

            using (var context = new EMSDBContext())
            {

                return context.Products.Where(x => x.Category.ID == CategoryID).OrderByDescending(x => x.ID).Take(pageSize).Include(x => x.Category).ToList();


            }
        }
        public List<Product> GetLetestProducts(int numberOfProducts)
        {

            using (var context = new EMSDBContext())
            {

                return context.Products.OrderByDescending(x => x.ID).Take(numberOfProducts).Include(x => x.Category).ToList();


            }
        }

        public void SaveProduct(Product product)
        {
            using (var context = new EMSDBContext())
            {
                context.Entry(product.Category).State = System.Data.Entity.EntityState.Unchanged;
                context.Products.Add(product);
                context.SaveChanges();

            }
        }

        public void UpdateProduct(Product product)
        {
            using (var context = new EMSDBContext())
            {
                context.Entry(product).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

            }
        }

        public void DeleteProduct(int ID)
        {
            using (var context = new EMSDBContext())
            {
                var product = context.Products.Find(ID);
                context.Products.Remove(product);
                context.SaveChanges();

            }
        }
    }
}
