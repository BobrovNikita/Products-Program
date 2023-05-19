using ProductsAzyavchikava.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class Sell
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Дата это обязательное поле")]
        [CheckDate(ErrorMessage = "Дата должна быть между сегодня и 10 лет вперед")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Способ оплаты обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Способ оплаты должен содержать от 3 до 50 символов")]
        public string PaymentMethod { get; set; }

        [Required(ErrorMessage = "ФИО продавца обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "ФИО продавца должно содержать от 3 до 50 символов")]
        public string FIOSalesman { get; set; }

        [Required(ErrorMessage = "Магазин это обязательное поле")]
        public Guid ShopId { get; set; }
        public Shop Shop { get; set; }

        

        public IEnumerable<CompositionSelling> CompositionSellings { get; set; }
    }
}
