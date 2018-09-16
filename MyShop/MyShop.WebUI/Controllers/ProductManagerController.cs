using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;




namespace MyShop.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {

        ProductRepository context;

        public ProductManagerController() {
            context = new ProductRepository();
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.Collection().ToList();
            return View(products);
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
            Product p = new Product();
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
        public ActionResult Edit(string id)
        {
            Product p = context.Find(id);
            if (p == null)
                return HttpNotFound();
            else
                return View( p );
        }

        // POST: ProductManager/Edit/5
        [HttpPost]
        public ActionResult Edit(Product p, string Id)
        {
            Product pToEdit = context.Find(Id);
            if (pToEdit == null)
                return HttpNotFound();
            else {
                if( !ModelState.IsValid)
                    return View(p);
                pToEdit.Category = p.Category;
                pToEdit.Description = p.Description;
                pToEdit.Image = p.Image;
                pToEdit.Name = p.Name;
                pToEdit.Price = p.Price;

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
