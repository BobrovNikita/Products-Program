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
    public class CompositionSellingRepository : BaseRepository, ICompositionSellingWithBaseRepository
    {
        public CompositionSellingRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(CompositionSellingViewModel model)
        {
            using (var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);

                new Common.ModelDataValidation().Validate(entity);

                context.Compositions.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(CompositionSellingViewModel model)
        {
            using (var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);

                context.Compositions.Remove(entity);
                context.SaveChanges();
            }
        }       

        public IEnumerable<CompositionSellingViewModel> GetAll()
        {
            using (var context = new ApplicationContext())
            {
                var entities = context.Compositions.Include(s => s.Sell).Include(p => p.Product).ToList();
                List<CompositionSellingViewModel> viewModels = new List<CompositionSellingViewModel>();
                foreach (var e in entities)
                {
                    viewModels.Add(ConvertToViewModel(e));
                }

                return viewModels;
            }
        }

        public IEnumerable<CompositionSellingViewModel> GetAllByValue(string value)
        {
            var entities = db.Compositions.Include(s => s.Sell).Include(p => p.Product)
                .Where(
                s => s.Product.Name.Contains(value) ||
                s.Count.ToString().Contains(value) ||
                s.Product.Cost.ToString().Contains(value)
                      );

            List<CompositionSellingViewModel> viewModels = new List<CompositionSellingViewModel>();
            foreach (var e in entities)
            {
                viewModels.Add(ConvertToViewModel(e));
            }

            return viewModels;
        }

        public IEnumerable<CompositionSellingViewModel> GettAllById(Guid id)
        {
            var entities = db.Compositions.Include(s => s.Sell).Include(p => p.Product)
                .Where(c => c.SellId == id);

            List<CompositionSellingViewModel> viewModels = new List<CompositionSellingViewModel>();
            foreach (var e in entities)
            {
                viewModels.Add(ConvertToViewModel(e));
            }

            return viewModels;
        }

        public CompositionSellingViewModel GetModel(Guid id)
        {
            var entity = db.Compositions.Include(s => s.Sell).Include(p => p.Product).First(s => s.Id == id);

            var viewModel = ConvertToViewModel(entity);

            return viewModel;
        }

        public void Update(CompositionSellingViewModel model)
        {
            using (var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);
                new Common.ModelDataValidation().Validate(entity);

                context.Compositions.Update(entity);
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

        private CompositionSelling ConvertToEntity(CompositionSellingViewModel model)
        {
            var entity = new CompositionSelling();
            entity.Id = model.Id;
            entity.ProductId = model.ProductId;
            entity.SellId = model.SellId;
            entity.Count = model.Count;

            return entity;
        }

        private CompositionSellingViewModel ConvertToViewModel(CompositionSelling model)
        {
            var viewModel = new CompositionSellingViewModel();
            viewModel.Id = model.Id;
            viewModel.SellId = model.SellId;
            viewModel.ProductId = model.ProductId;
            viewModel.Count = model.Count;
            viewModel.ProductCost = model.Product.Cost;
            viewModel.ProductName = model.Product.Name;
            viewModel.SellDate = model.Sell.Date;
            viewModel.Sum = model.Product.Cost * model.Count;

            return viewModel;
        }
    }
}
