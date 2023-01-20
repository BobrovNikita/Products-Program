using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class ProductViewModel
    {
        public Guid ProductId { get; set; }
        public Guid Product_TypeId { get; set; }
        public Guid StorageId { get; set; }

        [DisplayName("Название")]
        public string PName { get; set; }

        [DisplayName("Продукт")]
        public string PType_Name { get; set; }

        [DisplayName("Тип")]
        public string Product_Type { get; set; }

        [DisplayName("Артикул")]
        public string VendorCode { get; set; }

        [DisplayName("Штрих код")]
        public string Hatch { get; set; }

        [DisplayName("Цена")]
        public int Cost { get; set; }

        [DisplayName("НДС")]
        public int NDS { get; set; }

        [DisplayName("Наценка")]
        public int Markup { get; set; }

        [DisplayName("Рознич. Цена")]
        public int Retail_Price { get; set; }

        [DisplayName("Производство")]
        public string Production { get; set; }

        [DisplayName("Груз.мест за штуку")]
        public int Weight_Per_Price { get; set; }

        [DisplayName("Масса")]
        public int Weight { get; set; }

        [DisplayName("Наличие")]
        public bool Availability { get; set; }

        [DisplayName("Склад")]
        public int Number_Storage { get; set; }
    }
}
