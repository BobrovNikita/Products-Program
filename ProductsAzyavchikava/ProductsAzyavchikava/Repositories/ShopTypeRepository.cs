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
    public class ShopTypeRepository : BaseRepository, IRepository<Shop_TypeViewModel>
    {
        public ShopTypeRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(Shop_TypeViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Shop_Type model = new Shop_Type();
                model.Shop_TypeId = viewModel.Shop_TypeId;
                model.ShopId = viewModel.ShopId;
                model.Product_TypeId = viewModel.Product_TypeId;
                model.Shop_Count = viewModel.Shop_Count;

                new Common.ModelDataValidation().Validate(model);

                context.Shop_Types.Add(model);
                context.SaveChanges();
            }
            
        }

        public void Delete(Shop_TypeViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Shop_Type model = new Shop_Type();
                model.Shop_TypeId = viewModel.Shop_TypeId;
                context.Shop_Types.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<Shop_TypeViewModel> GetAll()
        {
            return db.Shop_Types.Include(p => p.Product_Type).Include(s => s.Shop).Select(o => new Shop_TypeViewModel
            {
                Shop_TypeId = o.Shop_TypeId,
                Product_TypeId = o.Product_TypeId,
                ShopId = o.ShopId,
                Product_Name = o.Product_Type.Product_Name,
                Product_Type = o.Product_Type.Type_Name,
                Shop_Name = o.Shop.Shop_Name,
                Shop_Count = o.Shop_Count
            }).ToList();
        }

        public IEnumerable<Shop_TypeViewModel> GetAllByValue(string value)
        {
            var result = db.Shop_Types.Include(p => p.Product_Type)
                                .Include(s => s.Shop)
                                .Where
                                (st => st.Shop_Count.ToString().Contains(value) || 
                                 st.Shop.Shop_Name.Contains(value) || 
                                 st.Product_Type.Type_Name.Contains(value) ||
                                 st.Product_Type.Product_Name.Contains(value)
                                 );

            return result.Select(o => new Shop_TypeViewModel
            {
                Shop_TypeId = o.Shop_TypeId,
                Product_TypeId = o.Product_TypeId,
                ShopId = o.ShopId,
                Product_Name = o.Product_Type.Product_Name,
                Product_Type = o.Product_Type.Type_Name,
                Shop_Name = o.Shop.Shop_Name,
                Shop_Count = o.Shop_Count
            }).ToList();
        }

        public Shop_TypeViewModel GetModel(Guid id)
        {
            var result = db.Shop_Types.Include(p => p.Product_Type).Include(s => s.Shop).First(st => st.Shop_TypeId== id);

            Shop_TypeViewModel model = new Shop_TypeViewModel();
            model.Shop_TypeId = result.Shop_TypeId;
            model.Product_TypeId = result.Product_TypeId;
            model.ShopId = result.ShopId;
            model.Product_Name = result.Product_Type.Product_Name;
            model.Product_Type = result.Product_Type.Type_Name;
            model.Shop_Name = result.Shop.Shop_Name;
            model.Shop_Count = result.Shop_Count;

            return model;
        }

        public void Update(Shop_TypeViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Shop_Type model = new Shop_Type();
                model.Shop_TypeId = viewModel.Shop_TypeId;
                model.ShopId = viewModel.ShopId;
                model.Product_TypeId = viewModel.Product_TypeId;
                model.Shop_Count = viewModel.Shop_Count;

                new Common.ModelDataValidation().Validate(model);

                context.Shop_Types.Update(model);
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
