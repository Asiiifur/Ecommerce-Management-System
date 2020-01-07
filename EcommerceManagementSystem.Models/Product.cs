﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceManagementSystem.Models
{
    public class Product : BaseModel
    {
        [Range(1, 100000000)]
        public decimal Price { get; set; }
        public int CategoryID { get; set; }
        public virtual  Category Category { get; set; }
        public string ImageURL { get; set; }
    }
}
