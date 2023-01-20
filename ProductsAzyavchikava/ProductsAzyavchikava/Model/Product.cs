using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class Product
    {
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Name is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 50")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Vendor code is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Vendor code must be between 3 and 50")]
        public string VendorCode { get; set; }

        [Required(ErrorMessage = "Hatch code is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Hatch code must be between 3 and 50")]
        public string Hatch { get; set; }

        [Required(ErrorMessage = "Cost is required field")]
        [Range(1, 1000, ErrorMessage = "Cost must be between 1 and 1000")]
        public int Cost { get; set; }

        [Required(ErrorMessage = "НДС is required field")]
        [Range(1, 1000, ErrorMessage = "НДС must be between 1 and 1000")]
        public int NDS { get; set; }

        [Required(ErrorMessage = "Markup is required field")]
        [Range(1, 1000, ErrorMessage = "Markup must be between 1 and 1000")]
        public int Markup { get; set; }

        [Required(ErrorMessage = "Retail price is required field")]
        [Range(1, 1000, ErrorMessage = "Retail price must be between 1 and 1000")]
        public int Retail_Price { get; set; }

        [Required(ErrorMessage = "Production is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Production must be between 3 and 50")]
        public string Production { get; set; }

        [Required(ErrorMessage = "Weight per price is required field")]
        [Range(1, 1000, ErrorMessage = "Weight per price must be between 1 and 1000")]
        public int Weight_Per_Price { get; set; }

        [Required(ErrorMessage = "Weight is required field")]
        [Range(1, 1000, ErrorMessage = "Weight must be between 1 and 1000")]
        public int Weight { get; set; }

        public bool Availability { get; set; }

        [Required(ErrorMessage = "Product Type ID is required field")]
        public Guid Product_TypeId { get; set; }

        [Required(ErrorMessage = "Storage ID is required field")]
        public Guid StorageId { get; set; }

        public Product_Type Product_Type { get; set; }
        public Storage Storage { get; set; }


        public IEnumerable<CompositionRequest> CompositionRequests { get; set; }
        public IEnumerable<ProductIntoShop> ProductIntoShops { get; set; }
    }
}
