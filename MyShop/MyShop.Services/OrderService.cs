﻿using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Services
{
    public class OrderService : IOrderService
    {
        IRepository<Order> OrderContext;
        public OrderService(IRepository<Order> _orderContext)
        {
            this.OrderContext = _orderContext;
        }

        public void CreateOrder(Order baseOrder, List<BasketItemViewModel> basketItems)
        {
           foreach( var item in basketItems){
                baseOrder.OrderItems.Add(new OrderItem() {
                    ProductId = item.Id,
                    Image = item.ImageUrl,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity
                });
            }
            OrderContext.Insert(baseOrder);
            OrderContext.Commit();

        }

        public List<Order> GetOrderList() {
            return OrderContext.Collection().ToList();
        }

        public Order GetOrder(string id) {
            return OrderContext.Find(id);
        }

        public void UpdateOrder(Order updatedOrder) {
            OrderContext.Update(updatedOrder);
            OrderContext.Commit();
        }

    }
}
