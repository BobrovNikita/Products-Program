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
    public class ProductController
    {
        private readonly IProductView _view;
        private readonly IRepository<ProductViewModel> _repository;
        private readonly IRepository<StorageViewModel> _storageRepository;
        private readonly IRepository<Product_TypeViewModel> _productTypeRepository;

        private BindingSource productBindingSource;
        private BindingSource productTypeBindingSource;
        private BindingSource storageBindingSource;

        private IEnumerable<ProductViewModel>? _products;
        private IEnumerable<Product_TypeViewModel>? _productsType;
        private IEnumerable<StorageViewModel>? _storages;

        public ProductController(IProductView view, IRepository<ProductViewModel> repository, IRepository<StorageViewModel> storageRepository, IRepository<Product_TypeViewModel> productTypeRepository)
        {
            _view = view;
            _repository = repository;
            _storageRepository = storageRepository;
            _productTypeRepository = productTypeRepository;

            productBindingSource = new BindingSource();
            productTypeBindingSource = new BindingSource();
            storageBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadProductTypeList();
            LoadCombobox();

            view.SetProductBindingSource(productBindingSource);
            view.SetStorageBindingSource(storageBindingSource);
            view.SetProductTypeBindingSource(productTypeBindingSource);

            _view.Show();
        }

        private void LoadProductTypeList()
        {
            _products = _repository.GetAll();
            productBindingSource.DataSource = _products;
        }

        private void LoadCombobox()
        {
            _storages = _storageRepository.GetAll();
            storageBindingSource.DataSource = _storages;

            _productsType = _productTypeRepository.GetAll();
            productTypeBindingSource.DataSource = _productsType;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.ProductTypeId = new Product_TypeViewModel();
            _view.StorageId = new StorageViewModel();
            _view.PName = string.Empty;
            _view.VendorCode = string.Empty;
            _view.Hatch = string.Empty;
            _view.Cost = -1;
            _view.NDS = -1;
            _view.Markup = -1;
            _view.Retail_Price = -1;
            _view.Production = string.Empty;
            _view.Weight_Per_Price = -1;
            _view.Weight = -1;
            _view.Availability = false;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            var model = new ProductViewModel();
            model.ProductId = _view.Id;
            model.Product_TypeId = _view.ProductTypeId.Id;
            model.StorageId = _view.StorageId.Id;
            model.PName = _view.PName;
            model.VendorCode = _view.VendorCode;
            model.Hatch = _view.Hatch;
            model.Cost = _view.Cost;
            model.NDS = _view.NDS;
            model.Markup = _view.Markup;
            model.Retail_Price = _view.Retail_Price;
            model.Production = _view.Production;
            model.Weight_Per_Price = _view.Weight_Per_Price;
            model.Weight = _view.Weight;
            model.Availability = _view.Availability;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Product edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Product added successfuly";
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
                var model = (ProductViewModel)productBindingSource.Current;

                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Product deleted successfuly";
                LoadProductTypeList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Product";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (ProductViewModel)productBindingSource.Current;
            _view.Id = model.ProductId;
            _view.ProductTypeId.Id = model.Product_TypeId;
            _view.StorageId.Id = model.StorageId;
            _view.PName = model.PName;
            _view.VendorCode = model.VendorCode;
            _view.Hatch = model.Hatch;
            _view.Cost = model.Cost;
            _view.NDS = model.NDS;
            _view.Markup = model.Markup;
            _view.Retail_Price = model.Retail_Price;
            _view.Production = model.Production;
            _view.Weight_Per_Price= model.Weight_Per_Price;
            _view.Weight= model.Weight;
            _view.Availability= model.Availability;
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
                _products = _repository.GetAllByValue(_view.searchValue);
            else
                _products = _repository.GetAll();

            productBindingSource.DataSource = _products;
        }
    }
}
