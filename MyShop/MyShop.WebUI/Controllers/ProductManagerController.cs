using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;




namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        ProductRepository context;
        ProductCategoryRepository productCategories;

        public ProductManagerController() {
            context = new ProductRepository();
            productCategories = new ProductCategoryRepository();
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductWithCategoryViewModel> pvml = new List<ProductWithCategoryViewModel>();
            List<Product> products = context.Collection().ToList();
            foreach (Product p in products)
                pvml.Add( new ProductWithCategoryViewModel(p, productCategories.Find(p.Category) ) );

            return View(pvml);
        }

        // GET: ProductManager/Details/5
        public ActionResult Details(string id)
        {
            Product p = context.Find(id);
            if (p == null)
                return HttpNotFound();
            else
                return View(p);
        }

        // GET: ProductManager/Create
        public ActionResult Create()
        {
            ProductManagerViewModel p = new ProductManagerViewModel(new Product(), productCategories.Collection().ToList() );
            return View( p );
        }

        // POST: ProductManager/Create
        [HttpPost]
        public ActionResult Create(Product product){
            if (!ModelState.IsValid)
                return View(product);
            else {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }

        // GET: ProductManager/Edit/5
        public ActionResult Edit(string id) {
            Product p = context.Find(id);

            if (p == null)
                return HttpNotFound();
            else {
                ProductManagerViewModel pvm = new ProductManagerViewModel(p, productCategories.Collection().ToList());
                return View(pvm);
            }
        }

        // POST: ProductManager/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductManagerViewModel pvm, string Id)
        {
            Product pToEdit = context.Find(pvm.Product.Id);
            if (pToEdit == null)
                return HttpNotFound();
            else {
                if( !ModelState.IsValid)
                    return View(pvm);
                pToEdit.Category = pvm.Product.Category;
                pToEdit.Description = pvm.Product.Description;
                pToEdit.Image = pvm.Product.Image;
                pToEdit.Name = pvm.Product.Name;
                pToEdit.Price = pvm.Product.Price;

                context.Commit();

                return RedirectToAction("Index");


            }
        }

        // GET: ProductManager/Delete/5
        public ActionResult Delete(string id)
        {
            Product p = context.Find(id);
            if (p == null)
                return HttpNotFound();
            else {
                return View(p);
            }

        }

        // POST: ProductManager/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(Product p, string id)
        {
            Product pToDelete = context.Find(id);
            if (pToDelete == null)
                return HttpNotFound();
            else {
                context.Delete(id);
                context.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}
