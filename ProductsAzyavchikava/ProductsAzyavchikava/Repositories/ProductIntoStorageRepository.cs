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
    public class ProductIntoStorageRepository : BaseRepository, IRepository<ProductIntoStorageViewModel>
    {
        public ProductIntoStorageRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(ProductIntoStorageViewModel model)
        {
            using (var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);

                new Common.ModelDataValidation().Validate(entity);

                context.ProductIntoStorages.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(ProductIntoStorageViewModel model)
        {
            using (var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);

                var models = context.ProductIntoStorages.Where(e => e.ProductId == entity.ProductId).Include(e => e.Storage).Include(s => s.Product);

                context.ProductIntoStorages.RemoveRange(models);
                context.SaveChanges();
            }
        }

        public IEnumerable<ProductIntoStorageViewModel> GetAll()
        {
            using (var context = new ApplicationContext())
            {
                var entities = context.ProductIntoStorages.Include(s => s.Product).Include(e => e.Storage).ToList();
                List<ProductIntoStorageViewModel> viewModels = new List<ProductIntoStorageViewModel>();
                foreach (var e in entities)
                {
                    viewModels.Add(ConvertToViewModel(e));
                };

                var groups = viewModels.GroupBy
                    (
                    p => new
                    {
                        p.ProductId,
                        p.StorageNumber,
                        p.StorageId
                    }
                    ).Select(g => new ProductIntoStorageViewModel
                    {
                        ProductId = g.Key.ProductId,
                        StorageId = g.Key.StorageId,
                        ProductName = g.Select(p => p.ProductName).First(),
                        Count = g.Select(p => p.Count).Sum(),
                        StorageNumber = g.Key.StorageNumber
                    });

                return groups;
            }
        }

        public IEnumerable<ProductIntoStorageViewModel> GetAllByValue(string value)
        {
            var entities = db.ProductIntoStorages.Include(p => p.Product).Include(s => s.Storage)
                .Where(
                pit => pit.Storage.Storage_Number.ToString().Contains(value) ||
                       pit.Product.Name.Contains(value) ||
                       pit.Count.ToString().Contains(value)
                );

            List<ProductIntoStorageViewModel> viewModels = new List<ProductIntoStorageViewModel>();
            foreach (var e in entities)
            {
                viewModels.Add(ConvertToViewModel(e));
            }

            return viewModels;
        }

        public ProductIntoStorageViewModel GetModel(Guid id)
        {
            var entity = db.ProductIntoStorages.Include(s => s.Product).Include(p => p.Storage).First(s => s.Id == id);

            var viewModel = ConvertToViewModel(entity);

            return viewModel;
        }

        public void Update(ProductIntoStorageViewModel model)
        {
            using (var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);
                new Common.ModelDataValidation().Validate(entity);

                context.ProductIntoStorages.Update(entity);
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

        private ProductIntoStorage ConvertToEntity(ProductIntoStorageViewModel model)
        {
            var entity = new ProductIntoStorage();
            entity.Id = model.Id;
            entity.ProductId = model.ProductId;
            entity.StorageId = model.StorageId;
            entity.Count = model.Count;

            return entity;
        }

        private ProductIntoStorageViewModel ConvertToViewModel(ProductIntoStorage model)
        {
            var viewModel = new ProductIntoStorageViewModel();
            viewModel.Id = model.Id;
            viewModel.StorageId = model.StorageId;
            viewModel.ProductId = model.ProductId;
            viewModel.Count = model.Count;
            viewModel.ProductName = model.Product.Name;
            viewModel.StorageNumber = model.Storage.Storage_Number;

            return viewModel;
        }
    }
}
