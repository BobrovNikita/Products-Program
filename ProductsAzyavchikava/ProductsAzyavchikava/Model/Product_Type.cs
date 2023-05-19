using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ProductsAzyavchikava.Model
{
    public class Product_Type
    {
        [Required]
        public Guid Product_TypeId { get; set; }

        [DisplayName("Название")]
        [Required(ErrorMessage = "Продукт это обязательное поле")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Название продукта должно быть от 3 до 50 символов")]
        public string Product_Name { get; set; }


        [DisplayName("Тип")]
        [Required(ErrorMessage = "Тип продукта это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Тип продукта должен быть от 3 до 50 символов")]
        public string Type_Name { get; set; }


        public IEnumerable<Shop_Type> Shop_Types { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
