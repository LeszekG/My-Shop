using MyShop.Core.Contracts;
using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyShop.WebUI.Controllers
{
    public class OrderManagerController : Controller
    {
        IOrderService orderService;

        public OrderManagerController(IOrderService orderService) {
            this.orderService = orderService;
        }

        // GET: OrderManager
        public ActionResult Index()
        {
            List<Order> orders = orderService.GetOrderList();
            return View(orders);
        }

        // GET: OrderManager
        public ActionResult UpdateOrder(string id) {
            ViewBag.StatusList = new List<string>() {
                "Order created",
                "Payment Processed",
                "Order Shipped",
                "Order Complete"
            };
            Order order = orderService.GetOrder(id);
            return View(order);
        }

        [HttpPost]
        public ActionResult UpdateOrder(Order updateOrder, string id) {
            Order order = orderService.GetOrder(id);

            order.OrerStatus = updateOrder.OrerStatus;
            orderService.UpdateOrder(order);

            return RedirectToAction("Index");
        }

    }
}