using ProductsAzyavchikava.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.Intefraces
{
    public interface IProduct_TypeView
    {
        Guid Product_Type_Id { get; set; }

        string Product_Type_Name { get; set; }
        string Product_Type_Type { get; set; }


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

        void SetProductTypeBindingSource(BindingSource source);
        void Show();


    }
}
