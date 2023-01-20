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
    public class ProductTypeRepository : BaseRepository, IRepository<Product_TypeViewModel>
    {
        public ProductTypeRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(Product_TypeViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Product_Type model = new Product_Type();
                model.Product_Name = viewModel.Name;
                model.Type_Name = viewModel.Type;

                new Common.ModelDataValidation().Validate(model);

                context.Product_Types.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(Product_TypeViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Product_Type model = new Product_Type();
                model.Product_TypeId = viewModel.Id;
                model.Product_Name = viewModel.Name;
                model.Type_Name = viewModel.Type;

                context.Product_Types.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<Product_TypeViewModel> GetAll()
        {
            return db.Product_Types.Select(o => new Product_TypeViewModel
            {
                Id = o.Product_TypeId,
                Name = o.Product_Name,
                Type = o.Type_Name
            }).ToList();
        }

        public IEnumerable<Product_TypeViewModel> GetAllByValue(string value)
        {
            var result = db.Product_Types.Where(p => p.Product_Name.Contains(value) || p.Type_Name.Contains(value));

            return result.Select(o => new Product_TypeViewModel
            {
                Id = o.Product_TypeId,
                Name = o.Product_Name,
                Type = o.Type_Name,
            }).ToList();
        }

        public Product_TypeViewModel GetModel(Guid id)
        {
            var result = db.Product_Types.First(p => p.Product_TypeId == id);

            Product_TypeViewModel viewModel = new Product_TypeViewModel();
            viewModel.Id = result.Product_TypeId;
            viewModel.Name = result.Product_Name;
            viewModel.Type = result.Type_Name;

            return viewModel;
        }

        public void Update(Product_TypeViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Product_Type model = new Product_Type();
                model.Product_TypeId = viewModel.Id;
                model.Product_Name = viewModel.Name;
                model.Type_Name = viewModel.Type;

                new Common.ModelDataValidation().Validate(model);

                context.Product_Types.Update(model);
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
