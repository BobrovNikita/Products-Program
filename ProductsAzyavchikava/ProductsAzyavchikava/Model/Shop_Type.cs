using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class Shop_Type
    {
        public Guid Shop_TypeId { get; set; }


        [Required(ErrorMessage = "Количество магазинов это обязательное поле")]
        [Range(1, 1000, ErrorMessage = "Количество магазинов должно быть от 1 до 1000")]
        public int Shop_Count { get; set; }


        [Required(ErrorMessage = "Тип продукта это обязательное поле")]
        public Guid Product_TypeId { get; set; }

        [Required(ErrorMessage = "Магазин это обязательное поле")]
        public Guid ShopId { get; set; }

        public Product_Type Product_Type { get; set; }
        public Shop Shop { get; set; }
    }
}
