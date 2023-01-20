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


        [Required(ErrorMessage = "Shop type count is required field")]
        [Range(1, 1000, ErrorMessage = "Shop count must be between 1 and 1000")]
        public int Shop_Count { get; set; }


        [Required(ErrorMessage = "Product Type ID is required field")]
        public Guid Product_TypeId { get; set; }

        [Required(ErrorMessage = "Shop ID is required field")]
        public Guid ShopId { get; set; }

        public Product_Type Product_Type { get; set; }
        public Shop Shop { get; set; }
    }
}
