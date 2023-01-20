using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class Product_TypeViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Тип")]
        public string Type { get; set; }
    }
}
