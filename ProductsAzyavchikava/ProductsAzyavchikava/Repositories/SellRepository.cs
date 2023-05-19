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
    public class SellRepository : BaseRepository, IRepository<SellViewModel>
    {
        public SellRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(SellViewModel model)
        {
            using (var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);

                new Common.ModelDataValidation().Validate(entity);

                context.Sells.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(SellViewModel model)
        {
            using (var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);

                context.Sells.Remove(entity);
                context.SaveChanges();
            }
        }

        public IEnumerable<SellViewModel> GetAll()
        {
            using(var context = new ApplicationContext())
            {
                var entities = context.Sells.Include(s => s.Shop).ToList();
                List<SellViewModel> viewModels = new List<SellViewModel>();
                foreach (var e in entities)
                {
                    viewModels.Add(ConvertToViewModel(e));
                }

                return viewModels;
            }
        }

        public IEnumerable<SellViewModel> GetAllByValue(string value)
        {
            var entities = db.Sells.Include(s => s.Shop)
                .Where(
                s => s.FIOSalesman.Contains(value) ||
                s.Shop.Shop_Name.Contains(value) ||
                s.PaymentMethod.Contains(value)
                      );

            List<SellViewModel> viewModels = new List<SellViewModel>();
            foreach (var e in entities)
            {
                viewModels.Add(ConvertToViewModel(e));
            }

            return viewModels;
        }

        public SellViewModel GetModel(Guid id)
        {
            var entity = db.Sells.Include(s => s.Shop).First(s => s.Id == id);

            var viewModel = ConvertToViewModel(entity);

            return viewModel;
        }

        public void Update(SellViewModel model)
        {
            using(var context = new ApplicationContext())
            {
                var entity = ConvertToEntity(model);
                new Common.ModelDataValidation().Validate(entity);

                context.Sells.Update(entity);
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


        private Sell ConvertToEntity(SellViewModel model)
        {
            var entity = new Sell();
            entity.Id = model.SellId;
            entity.ShopId = model.ShopId;
            entity.PaymentMethod = model.PaymentMethod;
            entity.Date = model.Date;
            entity.FIOSalesman = model.FIOSalesman;

            return entity;
        }

        private SellViewModel ConvertToViewModel(Sell model)
        {
            var viewModel = new SellViewModel();
            viewModel.SellId = model.Id;
            viewModel.ShopId = model.ShopId;
            viewModel.PaymentMethod = model.PaymentMethod;
            viewModel.Date = model.Date;
            viewModel.FIOSalesman = model.FIOSalesman;
            viewModel.ShopName = model.Shop.Shop_Name;

            return viewModel;
        }
    }
}
