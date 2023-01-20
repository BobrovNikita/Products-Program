using ProductsAzyavchikava.Views.Intefraces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductsAzyavchikava.Views
{
    public partial class MainView : Form, IMainView
    {
        public MainView()
        {
            InitializeComponent();

            InitializeBtnEvents();
        }

        private void InitializeBtnEvents()
        {
            CompositionRequestBtn.Click += delegate { LoadCompositionRequest?.Invoke(this, EventArgs.Empty); };
            ProductBtn.Click += delegate { LoadProduct?.Invoke(this, EventArgs.Empty); };
            Product_TypeBtn.Click += delegate { LoadProduct_Type?.Invoke(this, EventArgs.Empty); };
            ProductIntoShopBtn.Click += delegate { LoadProductIntoShop?.Invoke(this, EventArgs.Empty); };
            RequestBtn.Click += delegate { LoadRequest?.Invoke(this, EventArgs.Empty); };
            ShopBtn.Click += delegate { LoadShop?.Invoke(this, EventArgs.Empty); };
            Shop_TypeBtn.Click += delegate { LoadShop_Type?.Invoke(this, EventArgs.Empty); };
            StorageBtn.Click += delegate { LoadStorage?.Invoke(this, EventArgs.Empty); };
            FormClosed += delegate { Application.Exit(); };
        }

        public event EventHandler LoadCompositionRequest;
        public event EventHandler LoadProduct;
        public event EventHandler LoadProduct_Type;
        public event EventHandler LoadProductIntoShop;
        public event EventHandler LoadRequest;
        public event EventHandler LoadShop;
        public event EventHandler LoadShop_Type;
        public event EventHandler LoadStorage;
    }
}
