using Microsoft.EntityFrameworkCore;
using ProductsAzyavchikava.Model;
using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Repositories
{
    public class RequestRepository : BaseRepository, IRepository<RequestViewModel>
    {
        public RequestRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(RequestViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Request model = new Request();
                model.RequestId = viewModel.Id;
                model.ShopId = viewModel.ShopId;
                model.StorageId = viewModel.StorageId;
                model.Date = viewModel.Date;
                model.Products_Count = viewModel.Products_Count;
                model.Request_Cost = viewModel.Cost;
                model.Nds_Sum = viewModel.Nds_Sum;
                model.Cost_With_NDS = viewModel.Cost_With_NDS;
                model.Number_Packages = viewModel.Number_Packages;
                model.Weigh = viewModel.Weigh;
                model.Car = viewModel.Car;
                model.Driver = viewModel.Driver;

                new Common.ModelDataValidation().Validate(model);

                context.Requests.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(RequestViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Request model = new Request();

                model.RequestId = viewModel.Id;
                model.ShopId = viewModel.ShopId;
                model.StorageId = viewModel.StorageId;
                model.Date = viewModel.Date;
                model.Products_Count = viewModel.Products_Count;
                model.Request_Cost = viewModel.Cost;
                model.Nds_Sum = viewModel.Nds_Sum;
                model.Cost_With_NDS = viewModel.Cost_With_NDS;
                model.Number_Packages = viewModel.Number_Packages;
                model.Weigh = viewModel.Weigh;
                model.Car = viewModel.Car;
                model.Driver = viewModel.Driver;

                context.Requests.Remove(model);
                context.SaveChanges();
            }
            
        }

        public IEnumerable<RequestViewModel> GetAll()
        {
            return db.Requests.Include(s => s.Shop).Include(s => s.Storage).Select(o => new RequestViewModel
            {
                Id = o.RequestId,
                ShopId = o.Shop.ShopId,
                StorageId = o.StorageId,
                Shop_Name = o.Shop.Shop_Name,
                Shop_Adress = o.Shop.Shop_Adress,
                Storage_Number = o.Storage.Storage_Number,
                Date = o.Date,
                Products_Count = o.Products_Count,
                Cost = o.Request_Cost,
                Nds_Sum = o.Nds_Sum,
                Cost_With_NDS = o.Cost_With_NDS,
                Number_Packages = o.Number_Packages,
                Weigh = o.Weigh,
                Car = o.Car,
                Driver = o.Driver,
            }).ToList();
        }

        public IEnumerable<RequestViewModel> GetAllByValue(string value)
        {
            var result = db.Requests.Include(s => s.Shop)
                              .Include(s => s.Storage)
                              .Where
                              (r => r.Driver.Contains(value) ||
                               r.Car.Contains(value) || 
                               r.Weigh.ToString().Contains(value) ||
                               r.Shop.Shop_Name.Contains(value)
                               ).ToList();
            return result.Select(o => new RequestViewModel
            {
                Id = o.RequestId,
                ShopId = o.Shop.ShopId,
                StorageId = o.StorageId,
                Shop_Name = o.Shop.Shop_Name,
                Shop_Adress = o.Shop.Shop_Adress,
                Storage_Number = o.Storage.Storage_Number,
                Date = o.Date,
                Products_Count = o.Products_Count,
                Cost = o.Request_Cost,
                Nds_Sum = o.Nds_Sum,
                Cost_With_NDS = o.Cost_With_NDS,
                Number_Packages = o.Number_Packages,
                Weigh = o.Weigh,
                Car = o.Car,
                Driver = o.Driver,
            }).ToList();
        }

        public RequestViewModel GetModel(Guid id)
        {
            var result = db.Requests.Include(s => s.Shop).Include(s => s.Storage).First(r => r.RequestId == id);

            RequestViewModel model = new RequestViewModel();
            model.Id = result.RequestId;
            model.ShopId = result.ShopId;
            model.StorageId = result.StorageId;
            model.Date = result.Date;
            model.Shop_Name = result.Shop.Shop_Name;
            model.Shop_Adress = result.Shop.Shop_Adress;
            model.Storage_Number = result.Storage.Storage_Number;
            model.Products_Count = result.Products_Count;
            model.Cost = result.Request_Cost;
            model.Nds_Sum = result.Nds_Sum;
            model.Cost_With_NDS = result.Cost_With_NDS;
            model.Number_Packages = result.Number_Packages;
            model.Weigh = result.Weigh;
            model.Car = result.Car;
            model.Driver = result.Driver;

            return model;
        }

        public void Update(RequestViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Request model = new Request();
                model.RequestId = viewModel.Id;
                model.ShopId = viewModel.ShopId;
                model.StorageId = viewModel.StorageId;
                model.Date = viewModel.Date;
                model.Products_Count = viewModel.Products_Count;
                model.Request_Cost = viewModel.Cost;
                model.Nds_Sum = viewModel.Nds_Sum;
                model.Cost_With_NDS = viewModel.Cost_With_NDS;
                model.Number_Packages = viewModel.Number_Packages;
                model.Weigh = viewModel.Weigh;
                model.Car = viewModel.Car;
                model.Driver = viewModel.Driver;

                new Common.ModelDataValidation().Validate(model);

                context.Requests.Update(model);
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
