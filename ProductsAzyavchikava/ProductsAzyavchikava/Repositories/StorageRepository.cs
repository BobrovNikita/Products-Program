using ProductsAzyavchikava.Model;
using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Repositories
{
    public class StorageRepository : BaseRepository, IRepository<StorageViewModel>
    {
        public StorageRepository(ApplicationContext context) : base(context)
        {
        }

        public void Create(StorageViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Storage model = new Storage();
                model.Storage_Number = viewModel.Number;
                model.Storage_Adress= viewModel.Adress;
                model.Storage_Purpose= viewModel.Purpose;

                new Common.ModelDataValidation().Validate(model);

                context.Storages.Add(model);
                context.SaveChanges();
            }
        }

        public void Delete(StorageViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Storage model = new Storage();
                model.StorageId = viewModel.Id;
                model.Storage_Number = viewModel.Number;
                model.Storage_Adress = viewModel.Adress;
                model.Storage_Purpose = viewModel.Purpose;
                context.Storages.Remove(model);
                context.SaveChanges();
            }
        }

        public IEnumerable<StorageViewModel> GetAll()
        {
            return db.Storages.Select(o => new StorageViewModel
            {
                Id = o.StorageId,
                Number = o.Storage_Number,
                Adress = o.Storage_Adress,
                Purpose = o.Storage_Purpose,
            }).ToList();
        }

        public IEnumerable<StorageViewModel> GetAllByValue(string value)
        {
            var result = db.Storages.Where(s => s.Storage_Number.ToString().Contains(value) || s.Storage_Adress.Contains(value) || s.Storage_Purpose.Contains(value)).ToList();

            return result.Select(o => new StorageViewModel
            {
                Id = o.StorageId,
                Number = o.Storage_Number,
                Adress = o.Storage_Adress,
                Purpose = o.Storage_Purpose,
            }).ToList();
        }

        public StorageViewModel GetModel(Guid id)
        {
            var result = db.Storages.First(s => s.StorageId == id);

            var model = new StorageViewModel();
            model.Id = result.StorageId;
            model.Number = result.Storage_Number;
            model.Adress = result.Storage_Adress;
            model.Purpose = result.Storage_Purpose;

            return model;
        }

        public void Update(StorageViewModel viewModel)
        {
            using (var context = new ApplicationContext())
            {
                Storage model = new Storage();
                model.StorageId = viewModel.Id;
                model.Storage_Number = viewModel.Number;
                model.Storage_Adress = viewModel.Adress;
                model.Storage_Purpose = viewModel.Purpose;

                new Common.ModelDataValidation().Validate(model);

                context.Storages.Update(model);
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
