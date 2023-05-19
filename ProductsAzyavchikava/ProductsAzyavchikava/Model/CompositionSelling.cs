using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class CompositionSelling
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Количество это обязательное поле")]
        [Range(1, 10000, ErrorMessage = "Количество должно быть между 1 и 10000")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Товар это обязательное поле")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Продажа это обязательное поле")]
        public Guid SellId { get; set; }


        public Sell Sell { get; set; }
        public Product Product { get; set; }
    }
}
