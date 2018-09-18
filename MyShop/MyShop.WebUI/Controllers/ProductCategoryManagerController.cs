using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.Contracts;

namespace MyShop.WebUI.Controllers
{
    public class ProductCategoryManagerController : Controller
    {
        IRepository<ProductCategory> productCategoriesContext;

        public ProductCategoryManagerController(IRepository<ProductCategory>  _productCategoriesContext) {
            productCategoriesContext = _productCategoriesContext;
        }


        // GET: ProductManager
        public ActionResult Index() {
            List<ProductCategory> categories = productCategoriesContext.Collection().ToList();
            return View(categories);
        }

        // GET: ProductManager/Details/5
        public ActionResult Details(string id) {
            ProductCategory p = productCategoriesContext.Find(id);
            if (p == null)
                return HttpNotFound();
            else
                return View(p);
        }

        // GET: ProductManager/Create
        public ActionResult Create() {
            ProductCategory p = new ProductCategory();
            return View(p);
        }

        // POST: ProductManager/Create
        [HttpPost]
        public ActionResult Create(ProductCategory p) {
            if (!ModelState.IsValid)
                return View(p);
            else {
                productCategoriesContext.Insert(p);
                productCategoriesContext.Commit();

                return RedirectToAction("Index");
            }
        }

        // GET: ProductManager/Edit/5
        public ActionResult Edit(string id) {
            ProductCategory p = productCategoriesContext.Find(id);
            if (p == null)
                return HttpNotFound();
            else
                return View(p);
        }

        // POST: ProductManager/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductCategory p, string Id) {
            ProductCategory pToEdit = productCategoriesContext.Find(Id);
            if (pToEdit == null)
                return HttpNotFound();
            else {
                if (!ModelState.IsValid)
                    return View(p);
                pToEdit.Category = p.Category;
                productCategoriesContext.Commit();

                return RedirectToAction("Index");


            }
        }

        // GET: ProductManager/Delete/5
        public ActionResult Delete(string id) {
            ProductCategory p = productCategoriesContext.Find(id);
            if (p == null)
                return HttpNotFound();
            else {
                return View(p);
            }

        }

        // POST: ProductManager/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult Delete(ProductCategory p, string id) {
            ProductCategory pToDelete = productCategoriesContext.Find(id);
            if (pToDelete == null)
                return HttpNotFound();
            else {
                productCategoriesContext.Delete(id);
                productCategoriesContext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}