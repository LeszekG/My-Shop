using MyShop.Core.Contracts;
using MyShop.Core.Models;
using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> productContext, IRepository<Basket> basketContext)
        {
            this.productContext = productContext;
            this.basketContext = basketContext;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                    basket = basketContext.Find(basketId);
                else

                    basket = CreateNewBasket(httpContext);
            }
            return basket;
        }

        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }

        public void AddBasket(HttpContextBase contextBase, string productId)
        {
            Basket basket = GetBasket(contextBase, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                item = new BasketItem() { ProductId = productId, BasketId = basket.Id, Quantity = 1 };
                basket.BasketItems.Add(item);
            }
            else
                item.Quantity++;

            basketContext.Commit();
        }

        public void RemoveFromBasket(HttpContextBase contextBase, string itemId)
        {
            Basket basket = GetBasket(contextBase, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }

        public List<BasketItemViewModel> GetBasketItems(HttpContextBase contextBase)
        {
            Basket basket = GetBasket(contextBase, false);
            if (basket != null)
            {
                var result = (from b in basket.BasketItems
                              join p in productContext.Collection() on b.ProductId equals p.Id
                              select new BasketItemViewModel()
                              {
                                  Id = b.Id,
                                  Quantity = b.Quantity,
                                  ProductName = p.Name,
                                  ImageUrl = p.Image,
                                  Price = p.Price
                              }
                              ).ToList();
                return result;
            }
            return new List<BasketItemViewModel>();
        }

        public BasketSummaryViewModel GetBasketSummary(HttpContextBase contextBase)
        {
            /*
                        int BasketCount = 0;
                        decimal BasketTotal = 0;

                        List<BasketItemViewModel> items = GetBasketItems(contextBase);
                        foreach (BasketItemViewModel item in items)
                        {
                            BasketCount += item.Quantity;
                            BasketTotal += item.Quantity * item.Price;

                        }
                        return new BasketSummaryViewModel(BasketCount, BasketTotal);
            */
            Basket basket = GetBasket(contextBase, false);
            BasketSummaryViewModel bvm = new BasketSummaryViewModel(0, 0);
            if (basket != null)
            {
                int? basketCount = (from b in basket.BasketItems select b.Quantity).Sum();
                decimal? basketTotal = (from b in basket.BasketItems
                                        join p in productContext.Collection() on b.ProductId equals p.Id
                                        select b.Quantity*p.Price).Sum();

                bvm.BasketCount = basketCount ?? 0;
                bvm.BasketTotal = basketTotal ?? decimal.Zero;
            }
            return bvm;
        }

    }
}
