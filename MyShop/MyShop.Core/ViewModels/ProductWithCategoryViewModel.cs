using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class ProductWithCategoryViewModel
    {
        public Product Product;
        public ProductCategory ProductCategory;

        public ProductWithCategoryViewModel(Product product, ProductCategory productCategory) {
            Product = product;
            ProductCategory = productCategory;
        }
    }
}
