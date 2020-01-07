using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceManagementSystem.Models
{
    public class Category : BaseModel
    {
        public string ImageURL { get; set; }
        public List<Product> Products { get; set; }

        public bool IsFeatured { get; set; }

    }
}
