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

        public ActionResult Index()
        {
            List<ProductWithCategoryViewModel> pvml = new List<ProductWithCategoryViewModel>();
            List<Product> products = productContext.Collection().ToList();
            foreach (Product p in products)
                pvml.Add(new ProductWithCategoryViewModel(p, productCategoriesContext.Find(p.Category)));

            return View(pvml);

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