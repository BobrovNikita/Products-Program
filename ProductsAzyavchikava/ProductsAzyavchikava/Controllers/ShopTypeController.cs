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
    public class ShopTypeController
    {
        private readonly IShop_TypeView _view;
        private readonly IRepository<Shop_TypeViewModel> _repository;
        private readonly IRepository<Product_TypeViewModel> _productRepository;
        private readonly IRepository<ShopViewModel> _shopRepository;

        private BindingSource shopTypeBindingSource;
        private BindingSource shopBindingSource;
        private BindingSource productBindingSource;

        private IEnumerable<Shop_TypeViewModel>? _shopTypes;
        private IEnumerable<ShopViewModel>? _shops;
        private IEnumerable<Product_TypeViewModel>? _products;

        public ShopTypeController(IShop_TypeView view, IRepository<Shop_TypeViewModel> repository, IRepository<Product_TypeViewModel> productRepository, IRepository<ShopViewModel> shopRepository)
        {
            _view = view;
            _repository = repository;
            _productRepository = productRepository;
            _shopRepository = shopRepository;

            shopTypeBindingSource = new BindingSource();
            shopBindingSource = new BindingSource();
            productBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadProductTypeList();
            LoadCombobox();

            view.SetShopTypeBindingSource(shopTypeBindingSource);
            view.SetProductBindingSource(productBindingSource);
            view.SetShopBindingSource(shopBindingSource);

            _view.Show();
        }

        private void LoadProductTypeList()
        {
            _shopTypes = _repository.GetAll();
            shopTypeBindingSource.DataSource = _shopTypes;
        }

        private void LoadCombobox()
        {
            _products = _productRepository.GetAll();
            productBindingSource.DataSource = _products;

            _shops = _shopRepository.GetAll();
            shopBindingSource.DataSource = _shops;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.ProductId = new Product_TypeViewModel();
            _view.ShopId = new ShopViewModel();
            _view.Count = -1;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            var model = new Shop_TypeViewModel();
            model.Shop_TypeId = _view.Id;
            model.ShopId = _view.ShopId.Id;
            model.Product_TypeId = _view.ProductId.Id;
            model.Shop_Count = _view.Count;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Shop edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Shop added successfuly";
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
                var model = (Shop_TypeViewModel)shopTypeBindingSource.Current;

                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Shop deleted successfuly";
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
            var model = (Shop_TypeViewModel)shopTypeBindingSource.Current;
            _view.Id = model.Shop_TypeId;
            _view.ShopId.Id = model.ShopId;
            _view.ProductId.Id = model.Product_TypeId;
            _view.Count = model.Shop_Count;
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
                _shopTypes = _repository.GetAllByValue(_view.searchValue);
            else
                _shopTypes = _repository.GetAll();

            shopTypeBindingSource.DataSource = _shopTypes;
        }
    }
}
