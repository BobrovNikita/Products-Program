using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class ProductIntoStorageViewModel
    {
        public Guid Id { get; set; }

        public Guid StorageId { get; set; }
        public Guid ProductId { get; set; }

        [DisplayName("Товар")]
        public string ProductName { get; set; }


        [DisplayName("Номер склада")]
        public int StorageNumber { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

    }
}
