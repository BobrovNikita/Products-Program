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
    public class ProductRepository : BaseRepository, IRepository<ProductViewModel>
    {
        public ProductRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(ProductViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                Product model = new Product();
                model.ProductId = viewModel.ProductId;
                model.Product_TypeId = viewModel.Product_TypeId;
                model.StorageId = viewModel.StorageId;
                model.Name = viewModel.PName;
                model.VendorCode= viewModel.VendorCode;
                model.Hatch = viewModel.Hatch;
                model.Cost = viewModel.Cost;
                model.NDS = viewModel.NDS;
                model.Markup = viewModel.Markup;
                model.Retail_Price= viewModel.Retail_Price;
                model.Production = viewModel.Production;
                model.Weight_Per_Price= viewModel.Weight_Per_Price;
                model.Weight = viewModel.Weight;
                model.Availability = viewModel.Availability;

                new Common.ModelDataValidation().Validate(model);

                context.Products.Add(model);
                context.SaveChanges();
            }
            
        }

        public void Delete(ProductViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                Product model = new Product();
                model.ProductId = viewModel.ProductId;
                model.Product_TypeId = viewModel.Product_TypeId;
                model.StorageId = viewModel.StorageId;
                model.Name = viewModel.PName;
                model.VendorCode = viewModel.VendorCode;
                model.Hatch = viewModel.Hatch;
                model.Cost = viewModel.Cost;
                model.NDS = viewModel.NDS;
                model.Markup = viewModel.Markup;
                model.Retail_Price = viewModel.Retail_Price;
                model.Production = viewModel.Production;
                model.Weight_Per_Price = viewModel.Weight_Per_Price;
                model.Weight = viewModel.Weight;
                model.Availability = viewModel.Availability;

                context.Products.Remove(model); 
                context.SaveChanges();
            }
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            return db.Products.Include(pt => pt.Product_Type).Include(s => s.Storage)
                .Select(o => new ProductViewModel()
                {
                    ProductId = o.ProductId,
                    Product_TypeId = o.Product_TypeId,
                    StorageId= o.StorageId,
                    PName = o.Name,
                    PType_Name = o.Product_Type.Product_Name,
                    Product_Type = o.Product_Type.Type_Name,
                    VendorCode= o.VendorCode,
                    Hatch = o.Hatch,
                    Cost = o.Cost,
                    NDS = o.NDS,
                    Markup= o.Markup,
                    Retail_Price= o.Retail_Price,
                    Production= o.Production,
                    Weight_Per_Price= o.Weight_Per_Price,
                    Weight = o.Weight,
                    Availability= o.Availability,
                    Number_Storage = o.Storage.Storage_Number
                }).ToList();
        }

        public IEnumerable<ProductViewModel> GetAllByValue(string value)
        {
            var result = db.Products.Include(pt => pt.Product_Type)
                              .Include(s => s.Storage)
                              .Where
                              (p => p.Name.Contains(value) ||
                               p.Hatch.Contains(value) ||
                               p.VendorCode.Contains(value) ||
                               p.Production.Contains(value) ||
                               p.Product_Type.Product_Name.Contains(value) ||
                               p.Product_Type.Type_Name.Contains(value) ||
                               p.Storage.Storage_Number.ToString().Contains(value)
                              ).ToList();

            return result.Select(o => new ProductViewModel()
            {
                ProductId = o.ProductId,
                Product_TypeId = o.Product_TypeId,
                StorageId = o.StorageId,
                PName = o.Name,
                PType_Name = o.Product_Type.Product_Name,
                Product_Type = o.Product_Type.Type_Name,
                VendorCode = o.VendorCode,
                Hatch = o.Hatch,
                Cost = o.Cost,
                NDS = o.NDS,
                Markup = o.Markup,
                Retail_Price = o.Retail_Price,
                Production = o.Production,
                Weight_Per_Price = o.Weight_Per_Price,
                Weight = o.Weight,
                Availability = o.Availability,
                Number_Storage = o.Storage.Storage_Number
            }).ToList();
        }

        public ProductViewModel GetModel(Guid id)
        {
            var result = db.Products.Include(pt => pt.Product_Type).Include(s => s.Storage).First(p => p.ProductId== id);

            ProductViewModel model = new ProductViewModel();
            model.ProductId = result.ProductId;
            model.Product_TypeId = result.Product_TypeId;
            model.StorageId = result.StorageId;
            model.PName = result.Name;
            model.PType_Name = result.Product_Type.Product_Name;
            model.Product_Type = result.Product_Type.Type_Name;
            model.VendorCode = result.VendorCode;
            model.Hatch = result.Hatch;
            model.Cost = result.Cost;
            model.NDS = result.NDS;
            model.Markup = result.Markup;
            model.Retail_Price = result.Retail_Price;
            model.Production = result.Production;
            model.Weight_Per_Price = result.Weight_Per_Price;
            model.Weight = result.Weight;
            model.Availability = result.Availability;
            model.Number_Storage = result.Storage.Storage_Number;

            return model;
        }

        public void Update(ProductViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Product model = new Product();
                model.ProductId = viewModel.ProductId;
                model.Product_TypeId = viewModel.Product_TypeId;
                model.StorageId = viewModel.StorageId;
                model.Name = viewModel.PName;
                model.VendorCode = viewModel.VendorCode;
                model.Hatch = viewModel.Hatch;
                model.Cost = viewModel.Cost;
                model.NDS = viewModel.NDS;
                model.Markup = viewModel.Markup;
                model.Retail_Price = viewModel.Retail_Price;
                model.Production = viewModel.Production;
                model.Weight_Per_Price = viewModel.Weight_Per_Price;
                model.Weight = viewModel.Weight;
                model.Availability = viewModel.Availability;

                new Common.ModelDataValidation().Validate(model);

                context.Products.Update(model);
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
