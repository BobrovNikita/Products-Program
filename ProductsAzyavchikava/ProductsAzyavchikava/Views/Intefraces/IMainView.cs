using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.Intefraces
{
    public interface IMainView
    {
        event EventHandler LoadCompositionRequest;
        event EventHandler LoadProduct;
        event EventHandler LoadProduct_Type;
        event EventHandler LoadProductIntoShop;
        event EventHandler LoadRequest;
        event EventHandler LoadShop;
        event EventHandler LoadShop_Type;
        event EventHandler LoadStorage;
    }
}
