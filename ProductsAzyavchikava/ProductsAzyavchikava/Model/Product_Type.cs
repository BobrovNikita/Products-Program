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
        [Required(ErrorMessage = "Product name is required field")]
        [StringLength(50, MinimumLength =3, ErrorMessage = "Product name must be between 3 and 50")]
        public string Product_Name { get; set; }


        [DisplayName("Тип")]
        [Required(ErrorMessage = "Product type is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Product type must be between 3 and 50")]
        public string Type_Name { get; set; }


        public IEnumerable<Shop_Type> Shop_Types { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
