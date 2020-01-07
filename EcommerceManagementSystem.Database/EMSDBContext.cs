using EcommerceManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceManagementSystem.Database
{

    public class EMSDBContext : DbContext, IDisposable
    {
        public EMSDBContext() : base("EMSDatabaseConnection")
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Config> Configurations { get; set; }
    }
}
