using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class ShopViewModel
    {
        public Guid Id { get; set; }

        [DisplayName("Номер")]
        public int Identity { get; set; }

        [DisplayName("Название")]
        public string Name { get; set; }

        [DisplayName("Адрес")]
        public string Adress { get; set; }

        [DisplayName("Телефон")]
        public string Phone { get; set; }

        [DisplayName("Площадь")]
        public string Area { get; set; }
    }
}
