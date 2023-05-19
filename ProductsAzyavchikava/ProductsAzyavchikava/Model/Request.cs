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

        [Required(ErrorMessage = "Дата это обязательное поле")]
        [CheckDate(ErrorMessage = "Дата должна быть между сегодня и 10 лет вперед")]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Количество товаров это обязательное поле")]
        [Range(1, 10000, ErrorMessage = "Количество товаров должно быть между 1 и 10000")]
        public int Products_Count { get; set; }

        [Required(ErrorMessage = "Стоимость заявки это обязательное поле")]
        [Range(1, 10000, ErrorMessage = "Стоимость заявки должно быть между 1 и 10000")]
        public int Request_Cost { get; set; }

        [Required(ErrorMessage = "Количество грузовых мест это обязательное поле")]
        [Range(1, 5000, ErrorMessage = "Количество грузовых мест должно быть от 1 до 5000")]
        public int Number_Packages { get; set; }

        [Required(ErrorMessage = "Масса это обязательное поле")]
        [Range(1, 10000, ErrorMessage = "Масса должна быть от 1 до 10000")]
        public int Weigh { get; set; }

        [Required(ErrorMessage = "Машина это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название машины должно содержать от 3 до 50 символов")]
        public string Car { get; set; }

        [Required(ErrorMessage = "Водитель это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Водитель должен содержать от 3-х до 50 символов")]
        public string Driver { get; set; }


        [Required(ErrorMessage = "Дата поставки это обязательное поле")]
        [CheckDate(ErrorMessage = "Дата поставки должна быть от сегодняшнего дня и на 10 лет вперед")]
        public DateTime SupplyDate { get; set; }

        [Required(ErrorMessage = "Магазин это обязательное поле")]
        public Guid ShopId { get; set; }
        [Required(ErrorMessage = "Склад это обязательное поле")]
        public Guid StorageId { get; set; }

        
        public Storage Storage { get; set; }
        public Shop Shop { get; set; }




        public IEnumerable<CompositionRequest> CompositionRequests { get; set; }
       

    }
}
