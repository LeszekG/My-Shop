using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class BasketController : Controller
    {
        IRepository<Customer> customers;
        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService basketService, IOrderService orderService, IRepository<Customer> customers)
        {
            this.basketService = basketService;
            this.orderService = orderService;
            this.customers = customers;    
        }


        // GET: BasketCotroller
        public ActionResult Index()
        {
            return View(basketService.GetBasketItems(this.HttpContext));
        }

        public ActionResult AddToBasket(string id)
        {
            basketService.AddBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromBasket(string id)
        {
            basketService.RemoveFromBasket(this.HttpContext, id);
            return RedirectToAction("Index");
        }

        public PartialViewResult BasketSummary()
        {
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketSummary);
        }

        [Authorize]
        public ActionResult Checkout()
        {
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);
            if( customer != null) {
                Order order = new Order() {
                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    FirstName = customer.FirstName,
                    LasttName = customer.LastName,
                    ZipCode = customer.ZipCode

                };
                return View(order);
            }
            return RedirectToAction("Error");
        }

        
        [HttpPost]
        [Authorize]
        public ActionResult Checkout(Order order )
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrerStatus = "Order created";
            order.Email = User.Identity.Name;

            //process payment
            order.OrerStatus = "Payment passed";
            orderService.CreateOrder(order, basketItems);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("Thankyou", new { OrderId = order.Id} );

        }

        public ActionResult Thankyou(string OrderId)
        {
            ViewBag.OrderId = OrderId; 
            return View();
        }

    }
}