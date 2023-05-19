 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class Storage
    {
        public Guid StorageId { get; set; }

        [Required(ErrorMessage = "Номер склада это обязательное поле")]
        [Range(1, 1000, ErrorMessage = "Номер склада должен быть от 1 до 1000")]
        public int Storage_Number { get; set; }

        [Required(ErrorMessage = "Адрес склада это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Адрес склада должен содержать от 3 до 50 символов")]
        public string Storage_Adress { get; set; }

        [Required(ErrorMessage = "Назначение склада это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Назначение склада должно содержать от 3 до 50 символов")]
        public string Storage_Purpose { get; set; }


        public IEnumerable<Request> Requests { get; set;}
        public IEnumerable<ProductIntoStorage> ProductIntoStorages { get; set;}
        public IEnumerable<Product> Products { get; set;}
    }
}
