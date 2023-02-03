using ProductsAzyavchikava.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class Request
    {
        public Guid RequestId { get; set; }

        [Required(ErrorMessage = "Date is required field")]
        [CheckDate(ErrorMessage = "Date must be today or no more than 10 years")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Product count is required field")]
        [Range(1, 10000, ErrorMessage = "Product count must be between 1 and 10000")]
        public int Products_Count { get; set; }

        [Required(ErrorMessage = "Cost is required field")]
        [Range(1, 10000, ErrorMessage = "Cost must be between 1 and 10000")]
        public int Request_Cost { get; set; }

        [Required(ErrorMessage = "Number Packeges is required field")]
        [Range(1, 5000, ErrorMessage = "Number Packeges must be between 1 and 5000")]
        public int Number_Packages { get; set; }

        [Required(ErrorMessage = "Weight is required field")]
        [Range(1, 10000, ErrorMessage = "Weight must be between 1 and 10000")]
        public int Weigh { get; set; }

        [Required(ErrorMessage = "Car is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Car name must be between 3 and 50 symbols")]
        public string Car { get; set; }

        [Required(ErrorMessage = "Driver is required field")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Driver name must be between 3 and 50 symbols")]
        public string Driver { get; set; }

        [Required(ErrorMessage = "Shop ID is required field")]
        public Guid ShopId { get; set; }
        [Required(ErrorMessage = "Storage ID is required field")]
        public Guid StorageId { get; set; }

        
        public Storage Storage { get; set; }
        public Shop Shop { get; set; }




        public IEnumerable<CompositionRequest> CompositionRequests { get; set; }
       

    }
}
