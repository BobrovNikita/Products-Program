using ProductsAzyavchikava.Model;
using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Repositories
{
    public class ShopRepository : BaseRepository, IRepository<ShopViewModel>
    {
        public ShopRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(ShopViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                Shop model = new Shop();
                model.ShopId = viewModel.Id;
                model.Shop_Number = viewModel.Identity;
                model.Shop_Name = viewModel.Name;
                model.Shop_Adress = viewModel.Adress;
                model.Shop_Phone = viewModel.Phone;
                model.Shop_Area = viewModel.Area;

                if (!Regex.IsMatch(model.Shop_Phone, @"^(\+375)\((29|25|44|33)\) (\d{3})-(\d{2})-(\d{2})$"))
                    model.Shop_Phone = "";
                    

                new Common.ModelDataValidation().Validate(model);

                context.Shops.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(ShopViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                Shop model = new Shop();
                model.ShopId = viewModel.Id;
                model.Shop_Number = viewModel.Identity;
                model.Shop_Name = viewModel.Name;
                model.Shop_Adress = viewModel.Adress;
                model.Shop_Phone = viewModel.Phone;
                model.Shop_Area = viewModel.Area;
                context.Shops.Remove(model);
                context.SaveChanges();  
            }
            
        }

        public IEnumerable<ShopViewModel> GetAll()
        {
            return db.Shops.Select(o => new ShopViewModel
            {
                Id = o.ShopId,
                Name = o.Shop_Name,
                Identity = o.Shop_Number,
                Adress = o.Shop_Adress,
                Phone = o.Shop_Phone,
                Area = o.Shop_Area
            }).ToList();
        }

        public IEnumerable<ShopViewModel> GetAllByValue(string value)
        {
            var result = db.Shops.Where(s => s.Shop_Name.Contains(value) || s.Shop_Adress.Contains(value));

            return result.Select(o => new ShopViewModel
            {
                Id = o.ShopId,
                Name = o.Shop_Name,
                Identity = o.Shop_Number,
                Adress = o.Shop_Adress,
                Phone = o.Shop_Phone,
                Area = o.Shop_Area
            }).ToList();
        }

        public ShopViewModel GetModel(Guid id)
        {
            var result = db.Shops.First(s => s.ShopId== id);

            ShopViewModel model = new ShopViewModel();
            model.Id = result.ShopId;
            model.Identity = result.Shop_Number;
            model.Name = result.Shop_Name;
            model.Adress = result.Shop_Adress;
            model.Phone = result.Shop_Phone;
            model.Area = result.Shop_Area;

            return model;
        }

        public void Update(ShopViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Shop model = new Shop();
                model.ShopId = viewModel.Id;
                model.Shop_Number = viewModel.Identity;
                model.Shop_Name = viewModel.Name;
                model.Shop_Adress = viewModel.Adress;
                model.Shop_Phone = viewModel.Phone;
                model.Shop_Area = viewModel.Area;

                if (!Regex.IsMatch(model.Shop_Phone, @"^(\+375)\((29|25|44|33)\) (\d{3})-(\d{2})-(\d{2})$"))
                    model.Shop_Phone = "";

                new Common.ModelDataValidation().Validate(model);

                context.Shops.Update(model);
                context.SaveChanges();
            }
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
            }

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
