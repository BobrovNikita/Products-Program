using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class SellViewModel
    {
        public Guid SellId { get; set; }
        public Guid ShopId { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Способ оплаты")]
        public string PaymentMethod { get; set; }

        [DisplayName("ФИО продавца")]
        public string FIOSalesman { get; set; }


        [DisplayName("Название магазина")]
        public string ShopName { get; set; }
    }
}
