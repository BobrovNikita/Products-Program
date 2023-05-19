using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class CompositionSellingViewModel
    {
        public Guid Id { get; set; }
        public Guid SellId { get; set; }
        public Guid ProductId { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Название продукта")]
        public string ProductName { get; set; }

        [DisplayName("Цена товара")]
        public int ProductCost { get; set; }

        [DisplayName("Дата продажи")]
        public DateTime SellDate { get; set; }

        [DisplayName("Сумма")]
        public int Sum { get; set; }
    }
}
