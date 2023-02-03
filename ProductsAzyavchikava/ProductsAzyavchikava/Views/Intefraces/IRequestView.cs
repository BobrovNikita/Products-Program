using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.Intefraces
{
    public interface IRequestView
    {
        Guid Id { get; set; }
        ShopViewModel ShopId { get; set; }
        StorageViewModel StorageId { get; set; }
        DateTime Date { get; set; }
        int Product_Count { get; set; }
        int Cost { get; set; }
        int Number_Packages { get; set; }
        int Weigh { get; set; }
        string Car { get; set; }
        string Driver { get; set; }

        string searchValue { get; set; }
        DateTime firstDate { get; set; }
        DateTime lastDate { get; set; }
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
        event EventHandler SearchWithDateEvent;

        void SetRequestBindingSource(BindingSource source);
        void SetStorageBindingSource(BindingSource source);
        void SetShopBindingSource(BindingSource source);
        void Show();
    }
}
