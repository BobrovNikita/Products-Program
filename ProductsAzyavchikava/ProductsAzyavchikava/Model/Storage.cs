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

        [Required(ErrorMessage = "Storage number is required field")]
        [Range(1, 1000, ErrorMessage = "Storage number must be between 1 and 1000")]
        public int Storage_Number { get; set; }

        [Required(ErrorMessage = "Storage adress is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Storage adress must be between 3 and 50")]
        public string Storage_Adress { get; set; }

        [Required(ErrorMessage = "Storage Purpose is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Storage Purpose must be between 3 and 50")]
        public string Storage_Purpose { get; set; }


        public IEnumerable<Request> Requests { get; set;}
        public IEnumerable<Product> Products { get; set;}
    }
}
