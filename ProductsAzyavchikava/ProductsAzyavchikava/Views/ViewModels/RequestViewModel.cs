using ProductsAzyavchikava.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Views.ViewModels
{
    public class RequestViewModel
    {
        public Guid Id { get; set; }
        public Guid ShopId { get; set; }
        public Guid StorageId { get; set; }

        [DisplayName("Дата")]
        public DateTime Date { get; set; }

        [DisplayName("Дата поставки")]
        public DateTime SupplyDate { get; set; }

        [DisplayName("Магазин")]
        public string Shop_Name { get;set; }

        [DisplayName("Адрес")]
        public string Shop_Adress { get; set; }

        [DisplayName("Номер склада")]
        public int Storage_Number { get; set; }

        [DisplayName("Количество товара")]
        public int Products_Count { get; set; }

        [DisplayName("Цена")]
        public double Cost { get; set; }

        [DisplayName("Кол.груз.мест")]
        public int Number_Packages { get; set; }

        [DisplayName("Масса")]
        public int Weigh { get; set; }

        [DisplayName("Машина")]
        public string Car { get; set; }

        [DisplayName("Водитель")]
        public string Driver { get; set; }

    }
}
