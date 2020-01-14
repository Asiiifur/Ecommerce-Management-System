using EcommerceManagementSystem.Database;
using EcommerceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceManagementSystem.Manager
{
    public class ConfigurationsManager
    {
        #region Singleton
        public static ConfigurationsManager Instance
        {
            get
            {
                if (instance == null) instance = new ConfigurationsManager();
                return instance;
            }
        }
        private static ConfigurationsManager instance { get; set; }

        private ConfigurationsManager()
        {

        }
        #endregion


        public Config GetConfig(string Key)
        {
            using (var context = new EMSDBContext())
            {
                return context.Configurations.Find(Key);

            }
        }

        public int PageSize()
        {
            using (var context = new EMSDBContext())
            {
                var pageSizeConfig = context.Configurations.Find("PageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 5;
            }
        }

        public int ShopPageSize()
        {
            using (var context = new EMSDBContext())
            {
                var pageSizeConfig = context.Configurations.Find("ShopPageSize");

                return pageSizeConfig != null ? int.Parse(pageSizeConfig.Value) : 8;
            }
        }
    }
}
