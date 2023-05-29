using ProductsAzyavchikava.Repositories;
using ProductsAzyavchikava.Views;
using ProductsAzyavchikava.Views.Intefraces;
using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Controllers
{
    public class StorageCotnroller
    {
        private readonly IStorageView _view;
        private readonly IMainView _mainView;
        private readonly IRepository<StorageViewModel> _repository;

        private BindingSource storageBindingSource;

        private IEnumerable<StorageViewModel>? _storages;

        public StorageCotnroller(IStorageView view, IRepository<StorageViewModel> repository, IMainView mainView)
        {
            _view = view;
            _repository = repository;
            _mainView = mainView;

            storageBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.ProductIntoStorageOpen += ProductIntoStorageOpen;

            LoadProductTypeList();

            view.SetStorageBindingSource(storageBindingSource);

            _view.Show();
            
        }

        private void ProductIntoStorageOpen(object? sender, EventArgs e)
        {
            IProductIntoStorageView view = ProductIntoStorageView.GetInstance((MainView)_mainView);
            IRepository<ProductIntoStorageViewModel> repository = new ProductIntoStorageRepository(new ApplicationContext());
            IRepository<StorageViewModel> storageRepository = new StorageRepository(new ApplicationContext());
            IRepository<ProductViewModel> productRepository = new ProductRepository(new ApplicationContext());
            new ProductIntoStorageController(view, repository, productRepository, storageRepository, _mainView);
        }

        private void LoadProductTypeList()
        {
            _storages = _repository.GetAll();
            storageBindingSource.DataSource = _storages;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.Identity = -1;
            _view.Adress = string.Empty;
            _view.Purpose = string.Empty;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            var model = new StorageViewModel();
            model.Id = _view.Id;
            model.Number = _view.Identity;
            model.Adress = _view.Adress;
            model.Purpose = _view.Purpose;
            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Storage edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Storage added successfuly";
                }
                _view.IsSuccessful = true;
                LoadProductTypeList();
                CleanViewFields();
            }
            catch (Exception ex)
            {
                _view.IsSuccessful = false;
                _view.Message = ex.Message;
            }
        }

        private void DeleteSelected(object? sender, EventArgs e)
        {
            try
            {
                var model = (StorageViewModel)storageBindingSource.Current;

                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Storage deleted successfuly";
                LoadProductTypeList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Shop";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (StorageViewModel)storageBindingSource.Current;
            _view.Id = model.Id;
            _view.Identity = model.Number;
            _view.Adress = model.Adress;
            _view.Purpose = model.Purpose;
            _view.IsEdit = true;
        }

        private void Add(object? sender, EventArgs e)
        {
            _view.IsEdit = false;
        }

        private void Search(object? sender, EventArgs e)
        {
            bool emptyValue = String.IsNullOrWhiteSpace(_view.searchValue);

            if (emptyValue == false)
                _storages = _repository.GetAllByValue(_view.searchValue);
            else
                _storages = _repository.GetAll();

            storageBindingSource.DataSource = _storages;
        }
    }
}
