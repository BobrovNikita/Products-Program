using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.Intefraces
{
    public interface IProductView
    {
        Guid Id { get; set; }
        Product_TypeViewModel ProductTypeId { get; set; }
        StorageViewModel StorageId { get; set; }
        public string PName { get; set; }
        public string VendorCode { get; set; }
        public string Hatch { get; set; }
        public int Cost { get; set; }
        public int NDS { get; set; }
        public int Markup { get; set; }
        public int Retail_Price { get; set; }
        public string Production { get; set; }
        public int Weight_Per_Price { get; set; }
        public int Weight { get; set; }
        public bool Availability { get; set; }

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

        void SetProductBindingSource(BindingSource source);
        void SetStorageBindingSource(BindingSource source);
        void SetProductTypeBindingSource(BindingSource source);
        void Show();
    }
}
