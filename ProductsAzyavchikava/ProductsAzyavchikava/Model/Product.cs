using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Model
{
    public class Product
    {
        public Guid ProductId { get; set; }

        [Required(ErrorMessage = "Имя это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя должно быть от 3 до 50 символов")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Артикул это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Артикул должен быть от 3 до 50 символов")]
        public string VendorCode { get; set; }

        [Required(ErrorMessage = "Штрих код это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Штрих код должен быть от 3 до 50 символов")]
        public string Hatch { get; set; }

        [Required(ErrorMessage = "Цена это обязательное поле")]
        [Range(1, 1000, ErrorMessage = "Цена должна быть от 1 до 1000")]
        public double Cost { get; set; }

        [Required(ErrorMessage = "НДС это обязательное поле")]
        [Range(1, 1000, ErrorMessage = "НДС должен быть от 1 до 1000")]
        public double NDS { get; set; }

        [Required(ErrorMessage = "Наценка это обязательное поле")]
        [Range(1, 1000, ErrorMessage = "Наценка должна быть от 1 до 1000")]
        public double Markup { get; set; }

        [Required(ErrorMessage = "Производство это обязательное поле")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Производство должно быть от 3 до 50 символов")]
        public string Production { get; set; }

        [Required(ErrorMessage = "Масса грузового места за 1 штуку это обязательное поле")]
        [Range(1, 1000, ErrorMessage = "Масса грузового места за 1 штуку должно быть от 1 до 1000")]
        public int Weight_Per_Price { get; set; }

        [Required(ErrorMessage = "Масса за 1 штуку это обязательное поле")]
        [Range(1, 1000, ErrorMessage = "Масса за 1 штуку должна быть от 1 до 1000")]
        public int Weight { get; set; }

        public bool Availability { get; set; }

        [Required(ErrorMessage = "Разновидность товара это обязательное поле")]
        public Guid Product_TypeId { get; set; }

        [Required(ErrorMessage = "Склад это обязательное поле")]
        public Guid StorageId { get; set; }

        public Product_Type Product_Type { get; set; }
        public Storage Storage { get; set; }


        public IEnumerable<CompositionRequest> CompositionRequests { get; set; }
        public IEnumerable<CompositionSelling> CompositionSellings { get; set; }
        public IEnumerable<ProductIntoShop> ProductIntoShops { get; set; }
        public IEnumerable<ProductIntoStorage> ProductIntoStorages { get; set;}
    }
}
