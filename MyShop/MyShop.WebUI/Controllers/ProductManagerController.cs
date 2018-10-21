using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyShop.DataAccess.InMemory;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using MyShop.Core.Contracts;
using System.IO;

namespace MyShop.WebUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductManagerController : Controller
    {

        IRepository<Product> productContext;
        IRepository<ProductCategory> productCategoriesContext;

        public ProductManagerController(IRepository<Product> _productContext, IRepository<ProductCategory> _productCategoriesContext) {
            this.productContext = _productContext;
            this.productCategoriesContext = _productCategoriesContext;
        }


        // GET: ProductManager
        public ActionResult Index()
        {
            List<ProductWithCategoryViewModel> pvml = new List<ProductWithCategoryViewModel>();
            List<Product> products = productContext.Collection().ToList();
            foreach (Product p in products)
                pvml.Add( new ProductWithCategoryViewModel(p, productCategoriesContext.Find(p.Category) ) );

            return View(pvml);
        }

        // GET: ProductManager/Details/5
        public ActionResult Details(string id)
        {
            Product p = productContext.Find(id);
            if (p == null)
                return HttpNotFound();
            else
                return View(p);
        }

        // GET: ProductManager/Create
        public ActionResult Create()
        {
            ProductManagerViewModel p = new ProductManagerViewModel(new Product(), productCategoriesContext.Collection().ToList() );
            return View( p );
        }

        // POST: ProductManager/Create
        [HttpPost]
        public ActionResult Create(ProductManagerViewModel pmvm, HttpPostedFileBase file) {
            if (!ModelState.IsValid) {
                pmvm.Categories = productCategoriesContext.Collection().ToList();
                return View(pmvm);
            }
            else {
                if (file != null){
                    pmvm.Product.Image = pmvm.Product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + pmvm.Product.Image);
                }

                productContext.Insert(pmvm.Product);
                productContext.Commit();

                return RedirectToAction("Index");
            }
        }

        // GET: ProductManager/Edit/5
        public ActionResult Edit(string id) {
            Product p = productContext.Find(id);

            if (p == null)
                return HttpNotFound();
            else {
                ProductManagerViewModel pvm = new ProductManagerViewModel(p, productCategoriesContext.Collection().ToList());
                return View(pvm);
            }
        }

        // POST: ProductManager/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductManagerViewModel pvm, string Id, HttpPostedFileBase file)
        {
            Product pToEdit = productContext.Find(pvm.Product.Id);
            if (pToEdit == null)
                return HttpNotFound();
            else {
                if( !ModelState.IsValid)
                    return View(pvm);
                if (file != null)
                {
                    pToEdit.Image = pToEdit.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + pToEdit.Image);
                }
                pToEdit.Category = pvm.Product.Category;
                pToEdit.Description = pvm.Product.Description;
                pToEdit.Name = pvm.Product.Name;
                pToEdit.Price = pvm.Product.Price;

                productContext.Commit();

                return RedirectToAction("Index");


            }
        }

        // GET: ProductManager/Delete/5
        public ActionResult Delete(string id)
        {
            Product p = productContext.Find(id);
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
            Product pToDelete = productContext.Find(id);
            if (pToDelete == null)
                return HttpNotFound();
            else {
                productContext.Delete(id);
                productContext.Commit();
                return RedirectToAction("Index");
            }
        }
    }
}
