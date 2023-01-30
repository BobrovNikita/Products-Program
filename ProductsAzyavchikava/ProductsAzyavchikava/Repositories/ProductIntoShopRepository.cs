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
    public class ProductIntoShopRepository : BaseRepository, IRepository<ProductIntoShopViewModel>
    {
        public ProductIntoShopRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(ProductIntoShopViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                var model = new ProductIntoShop();
                model.ProductIntoShopId = viewModel.Id;
                model.ProductId = viewModel.ProductId;
                model.ShopId = viewModel.ShopId;
                model.Count = viewModel.Count;

                new Common.ModelDataValidation().Validate(model);

                context.ProductIntoShops.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(ProductIntoShopViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                ProductIntoShop model = new ProductIntoShop();
                model.ProductIntoShopId = viewModel.Id;
                model.ProductId = viewModel.ProductId;
                model.ShopId = viewModel.ShopId;
                model.Count = viewModel.Count;
                context.ProductIntoShops.Remove(model);
                context.SaveChanges();
            }
            
        }

        public IEnumerable<ProductIntoShopViewModel> GetAll()
        {
            return db.ProductIntoShops.Include(p => p.Product).Include(s => s.Shop).Select(o => new ProductIntoShopViewModel
            {
                Id = o.ProductIntoShopId,
                ShopId= o.ShopId,
                ProductId= o.ProductId,
                Count = o.Count,
                PName = o.Product.Name,
                Availability= o.Product.Availability,
                ShopName = o.Shop.Shop_Name,
                ShopNumber = o.Shop.Shop_Number
            }).ToList();
        }

        public IEnumerable<ProductIntoShopViewModel> GetAllByValue(string value)
        {
            var result = db.ProductIntoShops.Include(p => p.ProductId)
                                      .Include(s => s.ShopId)
                                      .Where(p => p.Count.ToString().Contains(value) ||
                                             p.Product.Name.Contains(value) ||
                                             p.Shop.Shop_Name.Contains(value) ||
                                             p.Shop.Shop_Number.ToString().Contains(value)
                                             );
            return result.Select(o => new ProductIntoShopViewModel
            {
                Id = o.ProductIntoShopId,
                ShopId = o.ShopId,
                ProductId = o.ProductId,
                Count = o.Count,
                PName = o.Product.Name,
                Availability = o.Product.Availability,
                ShopName = o.Shop.Shop_Name,
                ShopNumber = o.Shop.Shop_Number
            }).ToList();
        }

        public ProductIntoShopViewModel GetModel(Guid id)
        {
            var result = db.ProductIntoShops.Include(p => p.ProductId).Include(s => s.ShopId).First(p => p.ProductIntoShopId == id);

            var model = new ProductIntoShopViewModel();

            model.Id = result.ProductIntoShopId;
            model.ShopId = result.ShopId;
            model.ProductId = result.ProductId;
            model.Count = result.Count;
            model.PName = result.Product.Name;
            model.Availability = result.Product.Availability;
            model.ShopName = result.Shop.Shop_Name;
            model.ShopNumber = result.Shop.Shop_Number;

            return model;
        }

        public void Update(ProductIntoShopViewModel viewModel)
        {
            using(var context = new ApplicationContext())
            {
                var model = new ProductIntoShop();

                model.ProductIntoShopId = viewModel.Id;
                model.ShopId = viewModel.ShopId;
                model.ProductId = viewModel.ProductId;
                model.Count = viewModel.Count;

                new Common.ModelDataValidation().Validate(model);

                context.ProductIntoShops.Update(model);
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
