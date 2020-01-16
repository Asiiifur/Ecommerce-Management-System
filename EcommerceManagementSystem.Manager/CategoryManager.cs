using EcommerceManagementSystem.Database;
using EcommerceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace EcommerceManagementSystem.Manager
{
    public class CategoryManager
    {
        #region Singleton
        public static CategoryManager Instance
        {
            get
            {
                if (instance == null) instance = new CategoryManager();
                return instance;
            }
        }
        private static CategoryManager instance { get; set; }

        private CategoryManager()
        {

        }
        #endregion


        public Category GetCategory(int ID)
        {
            using (var context = new EMSDBContext())
            {
                return context.Categories.Find(ID);

            }
        }

        public int GetCategoriesCount(string search)
        {
            using (var context = new EMSDBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null &&
                         category.Name.ToLower().Contains(search.ToLower())).Count();

                }
                else
                {
                   return context.Categories.Count();
                }


            }
        }


        public List<Category> GetAllCategories()
        {
            using (var context = new EMSDBContext())
            {
                return context.Categories
                        .ToList();
            }
        }


        public List<Category> GetCategories (string search, int pageNO, int pageSize)
        {
            //int pageSize = 6;

            using (var context = new EMSDBContext())
            {
                if (!string.IsNullOrEmpty(search))
                {
                    return context.Categories.Where(category => category.Name != null &&
                         category.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(x => x.ID)
                        .Skip((pageNO - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
                else
                {
                    return context.Categories
                        .OrderBy(x => x.ID)
                        .Skip((pageNO - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }



            }
        }


        public List<Category> GetFeaturedCategories()
        {
            using (var context = new EMSDBContext())
            {
                return context.Categories.Where(x => x.IsFeatured && x.ImageURL != null).ToList(); 

            }
        }


        public void SaveCategory(Category category)
        {
            using (var context = new EMSDBContext())
            {
                context.Categories.Add(category);
                context.SaveChanges();

            }
        }


        public void UpdateCategory(Category category)
        {
            using (var context = new EMSDBContext())
            {
                context.Entry(category).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();

            }
        }


        public void DeleteCategory(int ID)
        {
            using (var context = new EMSDBContext())
            {
                var category = context.Categories.Where(x => x.ID == ID).Include(x => x.Products).FirstOrDefault();

                context.Products.RemoveRange(category.Products); //first delete products of this category
                context.Categories.Remove(category);
                context.SaveChanges();

            }
        }
    }
}
