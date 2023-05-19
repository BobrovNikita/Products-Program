using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.Intefraces
{
    public interface ICompositionSellingView
    {
        Guid Id { get; set; }
        SellViewModel SellId { get; set; }
        ProductViewModel ProductId { get; set; }

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
        event EventHandler CheckPrintEvent;

        void SetCompositionSellingBindingSource(BindingSource source);
        void SetSellBindingSource(BindingSource source);
        void SetProductBindingSource(BindingSource source);
        void Show();
    }
}
