using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class CompositionRequestViewModel
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid ProductId { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public int Sum { get; set; }

        [DisplayName("Количество товаров")]
        public int ProductCount { get; set; }

        [DisplayName("Название товара")]
        public string ProductName { get; set; }

        [DisplayName("Артикул")]
        public string ProductVenderCode { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }
        
        
    }
}
