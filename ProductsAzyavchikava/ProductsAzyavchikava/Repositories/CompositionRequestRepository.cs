using Azure.Core;
using Microsoft.EntityFrameworkCore;
using ProductsAzyavchikava.Model;
using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Repositories
{
    public class CompositionRequestRepository : BaseRepository, IRepository<CompositionRequestViewModel>
    {
        public CompositionRequestRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(CompositionRequestViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                var model = new CompositionRequest();

                model.CompositionRequestId= viewModel.Id;
                model.RequestId= viewModel.RequestId;
                model.ProductId= viewModel.ProductId;
                model.Count= viewModel.Count;
                model.Sum= viewModel.Sum;

                new Common.ModelDataValidation().Validate(model);

                context.CompositionRequests.Add(model);
                context.SaveChanges();
            }
            
        }

        public void Delete(CompositionRequestViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                var model = new CompositionRequest();

                model.CompositionRequestId = viewModel.Id;
                model.RequestId = viewModel.RequestId;
                model.ProductId = viewModel.ProductId;
                model.Count = viewModel.Count;
                model.Sum = viewModel.Sum;

                context.CompositionRequests.Remove(model);
                context.SaveChanges();
            }
            
        }

        public IEnumerable<CompositionRequestViewModel> GetAll()
        {
            return db.CompositionRequests.Include(p => p.Product).Include(r => r.Request).Select(o => new CompositionRequestViewModel
            {
                Id = o.CompositionRequestId,
                ProductId = o.Product.ProductId,
                RequestId = o.Request.RequestId,
                Count = o.Count,
                SumNds = o.Product.Cost * o.Product.NDS,
                CostWithNDS = (o.Product.Cost * o.Product.NDS) + o.Product.Cost,
                RetailPrice = (o.Product.Cost * o.Product.NDS + o.Product.Cost) / o.Request.Products_Count,
                Sum = ((o.Product.Cost * o.Product.NDS + o.Product.Cost) / o.Request.Products_Count) * o.Count,
                ProductName = o.Product.Name,
                ProductCount = o.Request.Products_Count,
                ProductVenderCode = o.Product.VendorCode,
                Date = o.Request.Date
            }).ToList();
        }

        public IEnumerable<CompositionRequestViewModel> GetAllByValue(string value)
        {
            var result = db.CompositionRequests.Include(p => p.Product)
                                         .Include(r => r.Request)
                                         .Where(c => c.Count.ToString().Contains(value) ||
                                                c.Sum.ToString().Contains(value) || 
                                                c.CompositionRequestId.ToString().Contains(value));
            return result.Select(o => new CompositionRequestViewModel
            {
                Id = o.CompositionRequestId,
                ProductId = o.Product.ProductId,
                RequestId = o.Request.RequestId,
                Count = o.Count,
                SumNds = o.Product.Cost * o.Product.NDS,
                CostWithNDS = (o.Product.Cost * o.Product.NDS) + o.Product.Cost,
                RetailPrice = (o.Product.Cost * o.Product.NDS + o.Product.Cost) / o.Request.Products_Count,
                Sum = ((o.Product.Cost * o.Product.NDS + o.Product.Cost) / o.Request.Products_Count) * o.Count,
                ProductName = o.Product.Name,
                ProductCount = o.Request.Products_Count,
                ProductVenderCode = o.Product.VendorCode,
                Date = o.Request.Date
            }).ToList();
        }

        public CompositionRequestViewModel GetModel(Guid id)
        {
            var result = db.CompositionRequests.Include(p => p.Product).Include(r => r.Request).First(c => c.CompositionRequestId== id);

            var model = new CompositionRequestViewModel();

            model.Id = result.CompositionRequestId;
            model.ProductId = result.Product.ProductId;
            model.RequestId = result.Request.RequestId;
            model.Count = result.Count;
            model.SumNds = result.Product.Cost * result.Product.NDS;
            model.CostWithNDS = (result.Product.Cost * result.Product.NDS) + result.Product.Cost;
            model.RetailPrice = (result.Product.Cost * result.Product.NDS + result.Product.Cost) / result.Request.Products_Count;
            model.Sum = ((result.Product.Cost * result.Product.NDS + result.Product.Cost) / result.Request.Products_Count) * result.Count;
            model.ProductName = result.Product.Name;
            model.ProductCount = result.Request.Products_Count;
            model.ProductVenderCode = result.Product.VendorCode;
            model.Date = result.Request.Date;

            return model;
        }

        public void Update(CompositionRequestViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new CompositionRequest();

                model.CompositionRequestId = viewModel.Id;
                model.RequestId = viewModel.RequestId;
                model.ProductId = viewModel.ProductId;
                model.Count = viewModel.Count;
                model.Sum = viewModel.Sum;

                context.CompositionRequests.Update(model);
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
