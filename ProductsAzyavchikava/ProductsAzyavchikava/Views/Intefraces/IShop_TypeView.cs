using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.Intefraces
{
    public interface IShop_TypeView
    {
        //Fields
        Guid Id { get; set; }
        ShopViewModel ShopId { get; set; }
        Product_TypeViewModel ProductId { get; set; }
        int Count { get; set; }

        string searchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccessful { get; set; }
        string Message { get; set; }

        //Events
        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetShopTypeBindingSource(BindingSource source);
        void SetProductBindingSource(BindingSource source);
        void SetShopBindingSource(BindingSource source);
        void Show();
    }
}
