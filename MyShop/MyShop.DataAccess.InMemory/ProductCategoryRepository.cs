using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategories;

        public ProductCategoryRepository() {
            productCategories = cache["productCategories"] as List<ProductCategory>;
            if (productCategories == null) {
                productCategories = new List<ProductCategory>();
                Insert(new ProductCategory() { Category = "cat 1" });
                Insert(new ProductCategory() { Category = "cat 2" });
                Commit();
            }


        }

        public void Commit() {
            cache["productCategories"] = productCategories;
        }

        public void Insert(ProductCategory p) {
            productCategories.Add(p);
        }

        public void Update(ProductCategory productCategory) {
            ProductCategory pToUpdate = productCategories.Find(p => p.Id == productCategory.Id);
            if (pToUpdate != null)
                pToUpdate = productCategory;
            else
                throw new Exception("Product not found");
        }

        public ProductCategory Find(string Id) {
            ProductCategory pf = productCategories.Find(p => p.Id == Id);
            if (pf != null)
                return pf;
            else
                throw new Exception("Product not found");
        }

        public void Delete(string Id) {
            ProductCategory pToDelete = productCategories.Find(p => p.Id == Id);
            if (pToDelete != null)
                productCategories.Remove(pToDelete);
            else
                throw new Exception("Product not found");
        }

        public IQueryable<ProductCategory> Collection() {
            return productCategories.AsQueryable();
        }


    }
}
