using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.ViewModels
{
    public class ProductManagerViewModel {

        public ProductManagerViewModel() {}

        public ProductManagerViewModel(Product product, IEnumerable<ProductCategory> productCategories) {
            this.Product = product;
            this.Categories = productCategories;
        }

        public Product Product { get; set; }
        public IEnumerable<ProductCategory> Categories { get; set; }


    }
}
