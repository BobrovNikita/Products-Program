using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class ProductIntoStorage
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Количество это обязательное поле")]
        [Range(1, 1000, ErrorMessage = "Количество должно быть от 1 до 1000")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Товар это обязательное поле")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Склад это обязательное поле")]
        public Guid StorageId { get; set; }
        public Storage Storage { get; set; }
        public Product Product { get; set; }    
        
    }
}
