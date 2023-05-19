using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.Intefraces
{
    public interface ISellView
    {

        Guid Id { get; set; }
        ShopViewModel ShopId { get; set; }
        DateTime Date { get; set; }

        string PaymentMethod { get; set; }
        string FIOSalesman { get; set; }
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

        void SetSellBindingSource(BindingSource source);
        void SetShopBindingSource(BindingSource source);
        void Show();
    }
}
