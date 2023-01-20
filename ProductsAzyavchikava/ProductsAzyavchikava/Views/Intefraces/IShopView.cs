using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.Intefraces
{
    public interface IShopView
    {

        //Fields
        Guid Id { get; set; }
        int Identity { get; set; }
        string Shop_Name { get; set; }
        string Adress { get; set; }
        string Phone { get; set; }
        string Area { get; set; }


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

        void SetShopBindingSource(BindingSource source);
        void Show();
    }
}
