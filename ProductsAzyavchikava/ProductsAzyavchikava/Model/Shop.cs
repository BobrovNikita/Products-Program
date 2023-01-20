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

        [Required(ErrorMessage = "Shop number is required field")]
        [Range(1, 1000, ErrorMessage = "Shop number must be between 1 and 1000")]
        public int Shop_Number { get; set; }

        [Required(ErrorMessage = "Shop name is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Shop name must be between 3 and 50")]
        public string Shop_Name { get; set; }

        [Required(ErrorMessage = "Shop adress is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Shop adress must be between 3 and 50")]
        public string Shop_Adress { get; set; }

        [Required(ErrorMessage = "Shop phone is required field")]
        public string Shop_Phone { get; set; }

        [Required(ErrorMessage = "Shop area is required field")]
        public string Shop_Area { get; set; }


        public IEnumerable<Shop_Type> Shop_Types { get; set; }
        public IEnumerable<Request> Requests { get; set; }
        public IEnumerable<ProductIntoShop> ProductsIntoShops { get; set; }
    }
}
