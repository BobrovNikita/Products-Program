using ProductsAzyavchikava.Repositories;
using ProductsAzyavchikava.Views.Intefraces;
using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductsAzyavchikava.Controllers
{
    public class RequestContorller
    {
        private readonly IRequestView _view;
        private readonly IRepository<RequestViewModel> _repository;
        private readonly IRepository<StorageViewModel> _storageRepository;
        private readonly IRepository<ShopViewModel> _shopRepository;

        private BindingSource requestBindingSource;
        private BindingSource shopBindingSource;
        private BindingSource storageBindingSource;

        private IEnumerable<RequestViewModel>? _requests;
        private IEnumerable<ShopViewModel>? _shops;
        private IEnumerable<StorageViewModel>? _storages;

        public RequestContorller(IRequestView view, IRepository<RequestViewModel> repository, IRepository<StorageViewModel> storageRepository, IRepository<ShopViewModel> shopRepository)
        {
            _view = view;
            _repository = repository;
            _storageRepository = storageRepository;
            _shopRepository = shopRepository;

            requestBindingSource = new BindingSource();
            shopBindingSource = new BindingSource();
            storageBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.SearchWithDateEvent += SearchWithDate;

            LoadProductTypeList();
            LoadCombobox();

            view.SetRequestBindingSource(requestBindingSource);
            view.SetStorageBindingSource(storageBindingSource);
            view.SetShopBindingSource(shopBindingSource);

            _view.Show();
        }

        private void LoadProductTypeList()
        {
            _requests = _repository.GetAll();
            requestBindingSource.DataSource = _requests;
        }

        private void LoadCombobox()
        {
            _storages = _storageRepository.GetAll();
            storageBindingSource.DataSource = _storages;

            _shops = _shopRepository.GetAll();
            shopBindingSource.DataSource = _shops;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.ShopId = new ShopViewModel();
            _view.StorageId = new StorageViewModel();
            _view.Date = DateTime.Now;
            _view.Product_Count = -1;
            _view.Cost = -1;
            _view.Number_Packages = -1;
            _view.Weigh = -1;
            _view.Car = string.Empty;
            _view.Driver = string.Empty;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.ShopId == null || _view.StorageId == null)
            {
                CleanViewFields();
                _view.Message = "Values are not specified in the combobox";
                return;
            }

            var model = new RequestViewModel();
            model.Id = _view.Id;
            model.ShopId = _view.ShopId.Id;
            model.StorageId = _view.StorageId.Id;
            model.Date = _view.Date;
            model.Products_Count = _view.Product_Count;
            model.Cost = _view.Cost;
            model.Number_Packages = _view.Number_Packages;
            model.Weigh = _view.Weigh;
            model.Car = _view.Car;
            model.Driver = _view.Driver;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Request edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Request added successfuly";
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
                var model = (RequestViewModel)requestBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Request deleted successfuly";
                LoadProductTypeList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Request";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (RequestViewModel)requestBindingSource.Current;
            _view.Id = model.Id;
            _view.ShopId.Id = model.ShopId;
            _view.StorageId.Id = model.StorageId;
            _view.Date = model.Date; 
            _view.Product_Count = model.Products_Count;
            _view.Cost = model.Cost;
            _view.Number_Packages = model.Number_Packages;
            _view.Weigh = model.Weigh / model.Products_Count;
            _view.Car = model.Car;
            _view.Driver = model.Driver;
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
                _requests = _repository.GetAllByValue(_view.searchValue);
            else
                _requests = _repository.GetAll();

            requestBindingSource.DataSource = _requests;
        }

        private void SearchWithDate(object? sender, EventArgs e)
        {
            _requests = _repository.GetAllByValue(_view.firstDate, _view.lastDate);

            requestBindingSource.DataSource = _requests;
        }
    }
}
