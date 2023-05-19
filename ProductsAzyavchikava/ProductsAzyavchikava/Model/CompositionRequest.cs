using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class CompositionRequest
    {
        public Guid CompositionRequestId { get; set; }


        [Required(ErrorMessage = "Количество это обязательное поле")]
        [Range(1, 10000, ErrorMessage = "Количество должно быть от 1 до 10000")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Сумма это обязательное поле")]
        [Range(1, 50000, ErrorMessage = "Сумма должна быть от 1 до 50000")]
        public int Sum { get; set; }

        [Required(ErrorMessage = "Заявка это обязательное поле")]
        public Guid RequestId { get; set; }

        [Required(ErrorMessage = "Товар это обязательное поле")]
        public Guid ProductId { get; set; }


        public Request Request { get; set; }
        public Product Product { get; set; }
    }
}
