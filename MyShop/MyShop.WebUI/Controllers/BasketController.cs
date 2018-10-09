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
        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService basketService, IOrderService orderService)
        {
            this.basketService = basketService;
            this.orderService = orderService;
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

        public ActionResult Checkout()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Checkout(Order order )
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            order.OrerStatus = "Order created";

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