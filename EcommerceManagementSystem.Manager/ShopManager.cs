using EcommerceManagementSystem.Database;
using EcommerceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceManagementSystem.Manager
{
   public class ShopManager
    {
        #region Singleton
        public static ShopManager Instance
        {
            get
            {
                if (instance == null) instance = new ShopManager();

                return instance;
            }
        }
        private static ShopManager instance { get; set; }

        private ShopManager()
        {
        }

        #endregion

        public int SaveOrder(Order order)
        {
            using (var context = new EMSDBContext())
            {
                context.Orders.Add(order);
                return context.SaveChanges();
            }
        }

    }
}
