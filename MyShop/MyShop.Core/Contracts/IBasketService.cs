using MyShop.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MyShop.Core.Contracts
{
    public interface IBasketService
    {
        void AddBasket(HttpContextBase contextBase, string productId);
        void RemoveFromBasket(HttpContextBase contextBase, string itemId);
        List<BasketItemViewModel> GetBasketItems(HttpContextBase contextBase);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase contextBase);
    }
}
