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

        public void Create(ProductViewModel model)
        {
            using(var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);

                new Common.ModelDataValidation().Validate(entity);

                context.Products.Add(entity);
                context.SaveChanges();
            }
            
        }

        public void Delete(ProductViewModel model)
        {
            using(var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);

                context.Products.Remove(entity); 
                context.SaveChanges();
            }
        }

        public IEnumerable<ProductViewModel> GetAll()
        {
            using(var context = new ApplicationContext())
            {
                var entities = context.Products.Include(pt => pt.Product_Type).Include(s => s.Storage).ToList();
                List<ProductViewModel> viewModels = new List<ProductViewModel>();
                foreach(var e in entities)
                {
                    viewModels.Add(ConvertToViewModel((e)));
                }

                return viewModels;
            }
            
        }

        public IEnumerable<ProductViewModel> GetAllByValue(string value)
        {
            var entities = db.Products.Include(pt => pt.Product_Type)
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

            List<ProductViewModel> viewModels = new List<ProductViewModel>();

            foreach (var e in entities)
            {
                viewModels.Add(ConvertToViewModel((e)));
            }

            return viewModels;
        }

        public ProductViewModel GetModel(Guid id)
        {
            var entity = db.Products.Include(pt => pt.Product_Type).Include(s => s.Storage).First(p => p.ProductId== id);

            var viewModel = ConvertToViewModel(entity);

            return viewModel;
        }

        public void Update(ProductViewModel model)
        {
            using (var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);
                new Common.ModelDataValidation().Validate(entity);

                context.Products.Update(entity);
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

        private Product ConvertToEntity(ProductViewModel model)
        {
            var entity = new Product();
            entity.ProductId = model.ProductId;
            entity.Product_TypeId = model.Product_TypeId;
            entity.StorageId = model.StorageId;
            entity.Name = model.PName;
            entity.VendorCode = model.VendorCode;
            entity.Hatch = model.Hatch;
            entity.Cost = model.Cost;
            entity.NDS = model.NDS;
            entity.Markup = model.Markup;
            entity.Production = model.Production;
            entity.Weight_Per_Price = model.Weight_Per_Price;
            entity.Weight = model.Weight;
            entity.Availability = model.Availability;

            return entity;
        }

        private ProductViewModel ConvertToViewModel(Product model)
        {
            var viewModel = new ProductViewModel();
            viewModel.ProductId = model.ProductId;
            viewModel.Product_TypeId = model.Product_TypeId;
            viewModel.StorageId = model.StorageId;
            viewModel.PName = model.Name;
            viewModel.PType_Name = model.Product_Type.Product_Name;
            viewModel.Product_Type = model.Product_Type.Type_Name;
            viewModel.VendorCode = model.VendorCode;
            viewModel.Hatch = model.Hatch;
            viewModel.Cost = model.Cost;
            viewModel.NDS = model.NDS;
            viewModel.Markup = model.Markup;
            viewModel.Retail_Price = (model.Cost + (model.Cost / 100 * model.NDS)) + ((model.Cost + (model.Cost / 100 * model.NDS))/100 * model.Markup);
            viewModel.Production = model.Production;
            viewModel.Weight_Per_Price = model.Weight_Per_Price;
            viewModel.Weight = model.Weight;
            viewModel.Availability = model.Availability;
            viewModel.Number_Storage = model.Storage.Storage_Number;

            return viewModel;
        }
    }
}
