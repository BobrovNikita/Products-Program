using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class Shop_TypeViewModel
    {
        public Guid Shop_TypeId { get; set; }
        public Guid Product_TypeId { get; set; }
        public Guid ShopId { get; set; }


        [DisplayName("Кол-во магазинов")]
        public int Shop_Count { get; set; }

        [DisplayName("Продукт")]
        public string Product_Name { get; set; }

        [DisplayName("Тип")]
        public string Product_Type { get; set; }

        [DisplayName("Магазин")]
        public string Shop_Name { get; set; }
    }
}
