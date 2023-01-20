using Microsoft.EntityFrameworkCore;
using ProductsAzyavchikava.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Repositories
{
    public class ProductIntoShopRepository : BaseRepository, IRepository<ProductIntoShop>
    {
        public ProductIntoShopRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(ProductIntoShop model)
        {
            db.ProductIntoShops.Add(model);
        }

        public void Delete(ProductIntoShop model)
        {
            db.ProductIntoShops.Remove(model);
        }

        public IEnumerable<ProductIntoShop> GetAll()
        {
            return db.ProductIntoShops.Include(p => p.ProductId).Include(s => s.ShopId).ToList();
        }

        public IEnumerable<ProductIntoShop> GetAllByValue(string value)
        {
            return db.ProductIntoShops.Include(p => p.ProductId)
                                      .Include(s => s.ShopId)
                                      .Where(p => p.Count.ToString().Contains(value) || p.Product.Name.Contains(value)).ToList();
        }

        public ProductIntoShop GetModel(Guid id)
        {
            return db.ProductIntoShops.Include(p => p.ProductId).Include(s => s.ShopId).First(p => p.ProductIntoShopId == id);
        }

        public void Update(ProductIntoShop model)
        {
            db.ProductIntoShops.Update(model);
        }

        public void Save()
        {
            db.SaveChanges();
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
