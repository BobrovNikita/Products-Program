using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class StorageViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Номер склада")]
        public int Number { get; set; }

        [DisplayName("Адрес")]
        public string Adress { get; set; }

        [DisplayName("Назначение")]
        public string Purpose { get; set; }
    }
}
