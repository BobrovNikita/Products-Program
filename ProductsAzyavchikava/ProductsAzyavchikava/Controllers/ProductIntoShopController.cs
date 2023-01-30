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
    public class ProductIntoShopController
    {
        private readonly IProductIntoShopView _view;
        private readonly IRepository<ProductIntoShopViewModel> _repository;
        private readonly IRepository<ProductViewModel> _productRepository;
        private readonly IRepository<ShopViewModel> _shopRepository;

        private BindingSource ProductIntoShopBindingSource;
        private BindingSource ProductBindingSource;
        private BindingSource ShopBindingSource;

        private IEnumerable<ProductIntoShopViewModel>? _productsInShop;
        private IEnumerable<ProductViewModel>? _products;
        private IEnumerable<ShopViewModel>? _shops;

        public ProductIntoShopController(IProductIntoShopView view, IRepository<ProductIntoShopViewModel> repository, IRepository<ProductViewModel> productRepository, IRepository<ShopViewModel> shopRepository)
        {
            _view = view;
            _repository = repository;
            _productRepository = productRepository;
            _shopRepository = shopRepository;

            ProductIntoShopBindingSource = new BindingSource();
            ProductBindingSource = new BindingSource();
            ShopBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadProductTypeList();
            LoadCombobox();

            view.SetProductIntoShopBindingSource(ProductIntoShopBindingSource);
            view.SetShopBindingSource(ShopBindingSource);
            view.SetProductBindingSource(ProductBindingSource);

            _view.Show();
        }

        private void LoadProductTypeList()
        {
            _productsInShop = _repository.GetAll();
            ProductIntoShopBindingSource.DataSource = _productsInShop;
        }

        private void LoadCombobox()
        {
            _products = _productRepository.GetAll();
            ProductBindingSource.DataSource = _products;

            _shops = _shopRepository.GetAll();
            ShopBindingSource.DataSource = _shops;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.ShopId = new ShopViewModel();
            _view.ProductId = new ProductViewModel();
            _view.Count = -1;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.ShopId == null || _view.ProductId == null)
            {
                CleanViewFields();
                _view.Message = "Values are not specified in the combobox";
                return;
            }

            var model = new ProductIntoShopViewModel();
            model.Id = _view.Id;
            model.ShopId = _view.ShopId.Id;
            model.ProductId = _view.ProductId.ProductId;
            model.Count = _view.Count;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Product into shop edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Product into shop added successfuly";
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
                var model = (ProductIntoShopViewModel)ProductIntoShopBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Product into shop deleted successfuly";
                LoadProductTypeList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Product into shop";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (ProductIntoShopViewModel)ProductIntoShopBindingSource.Current;
            _view.Id = model.Id;
            _view.ShopId.Id = model.ShopId;
            _view.ProductId.ProductId = model.ProductId;
            _view.Count = model.Count;
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
                _productsInShop = _repository.GetAllByValue(_view.searchValue);
            else
                _productsInShop = _repository.GetAll();

            ProductIntoShopBindingSource.DataSource = _productsInShop;
        }
    }
}
