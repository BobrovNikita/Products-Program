using System.ComponentModel;

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
        public double Sum { get; set; }

        [DisplayName("Сумма НДС")]
        public double SumNds { get; set; }

        [DisplayName("Цена с НДС")]
        public double CostWithNDS { get; set; }

        [DisplayName("Розничная цена")]
        public double RetailPrice { get; set; }

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
