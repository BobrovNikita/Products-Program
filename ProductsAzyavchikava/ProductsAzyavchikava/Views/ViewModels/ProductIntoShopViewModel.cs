using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class ProductIntoShopViewModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid ShopId { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Продукт")]
        public string PName { get; set; }

        [DisplayName("Наличие")]
        public bool Availability { get; set; }

        [DisplayName("Магазин")]
        public string ShopName { get; set; }

        [DisplayName("Номер магазина")]
        public int ShopNumber { get; set; }
    }
}
