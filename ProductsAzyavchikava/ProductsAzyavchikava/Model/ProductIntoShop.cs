using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class ProductIntoShop
    {
        public Guid ProductIntoShopId { get; set; }

        [Required(ErrorMessage = "Count is required field")]
        [Range(1, 10000, ErrorMessage = "Count must be between 1 and 10000")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Product ID is required field")]
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Shop ID is required field")]
        public Guid ShopId { get; set; }

        public Product Product { get; set; }
        public Shop Shop { get; set; }
    }
}
