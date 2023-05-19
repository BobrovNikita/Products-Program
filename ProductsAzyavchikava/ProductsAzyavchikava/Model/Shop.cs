using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class Shop
    {
        public Guid ShopId { get; set; }

        [Required(ErrorMessage = "Номер магазина это обязательное поле")]
        [Range(1, 1000, ErrorMessage = "Номер магазина должен быть от 1 до 1000")]
        public int Shop_Number { get; set; }

        [Required(ErrorMessage = "Название магазина это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название магазина должно содержать от 3 до 50 символов")]
        public string Shop_Name { get; set; }

        [Required(ErrorMessage = "Адрес магазина это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Адрес магазина должен содержать от 3 до 50 символов")]
        public string Shop_Adress { get; set; }

        [Required(ErrorMessage = "Номер телефона магазина это обязательное поле")]
        public string Shop_Phone { get; set; }

        [Required(ErrorMessage = "Площадь магазина это обязательное поле")]
        public string Shop_Area { get; set; }


        public IEnumerable<Shop_Type> Shop_Types { get; set; }
        public IEnumerable<Request> Requests { get; set; }
        public IEnumerable<ProductIntoShop> ProductsIntoShops { get; set; }
        public IEnumerable<Sell> Sells { get; set; }
    }
}
