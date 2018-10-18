using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class HomeController : Controller
    {
        IRepository<Product> productContext;
        IRepository<ProductCategory> productCategoriesContext;

        public HomeController(IRepository<Product> _productContext, IRepository<ProductCategory> _productCategoriesContext)
        {
            this.productContext = _productContext;
            this.productCategoriesContext = _productCategoriesContext;
        }

        public ActionResult Index(string Category = null)
        {
            List<Product> products;



            if (Category == null)
                products = productContext.Collection().ToList();
            else 
//                ProductCategory pcat = productCategoriesContext.Collection().FirstOrDefault(pc => pc.Category == Category);
                
                products = productContext.Collection().Where(p => p.Category == Category).ToList();
            

            ProducListViewModel model = new ProducListViewModel();
            model.Products = products;
            model.Categories = productCategoriesContext.Collection().ToList();

            return View(model);
        }

        public ActionResult Details(string Id)
        {
            Product product = productContext.Find(Id);
            if (product == null)
                return HttpNotFound();
            else
                return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}