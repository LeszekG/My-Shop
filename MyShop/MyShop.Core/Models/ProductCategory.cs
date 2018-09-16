﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class ProductCategory
    {
        [Key]
        public String Id { get; set; }

        //[StringLength(20)]
        //[Display(Name = "Product group name")]
//        [Required]
        public String Category { get; set; }

        public ProductCategory(){
            this.Id = Guid.NewGuid().ToString();
        }
    }
}
