using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class CompositionRequest
    {
        public Guid CompositionRequestId { get; set; }


        [Required(ErrorMessage = "Count is required field")]
        [Range(1, 10000, ErrorMessage = "Count must be between 1 and 10000")]
        public int Count { get; set; }

        [Required(ErrorMessage = "Sum is required field")]
        [Range(1, 50000, ErrorMessage = "Sum must be between 1 and 50000")]
        public int Sum { get; set; }

        [Required(ErrorMessage = "Request ID is required field")]
        public Guid RequestId { get; set; }

        [Required(ErrorMessage = "Product ID is required field")]
        public Guid ProductId { get; set; }


        public Request Request { get; set; }
        public Product Product { get; set; }
    }
}
