using ProductsAzyavchikava.Model;
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
    public class MainController
    {
        private readonly IMainView _mainView;

        public MainController(IMainView mainView)
        {
            _mainView = mainView;

            _mainView.LoadCompositionRequest += LoadCompositionRequest;
            _mainView.LoadProduct += LoadProduct;
            _mainView.LoadProduct_Type += LoadProduct_Type;
            _mainView.LoadProductIntoShop += LoadProductIntoShop;
            _mainView.LoadRequest += LoadRequest;
            _mainView.LoadShop += LoadShop;
            _mainView.LoadShop_Type += LoadShop_Type;
            _mainView.LoadStorage += LoadStorage;
            _mainView.LoadSell += LoadSell;
            _mainView.LoadCompositionSell += LoadCompositionSell;
            _mainView.LoadProductIntoStorage += LoadProductIntoStorage;
        }

        private void LoadProductIntoStorage(object? sender, EventArgs e)
        {
            IProductIntoStorageView view = ProductIntoStorageView.GetInstance((MainView)_mainView);
            IRepository<ProductIntoStorageViewModel> repository = new ProductIntoStorageRepository(new ApplicationContext());
            IRepository<StorageViewModel> storageRepository = new StorageRepository(new ApplicationContext());
            IRepository<ProductViewModel> productRepository = new ProductRepository(new ApplicationContext());
            new ProductIntoStorageController(view, repository, productRepository, storageRepository);
        }

        private void LoadCompositionSell(object? sender, EventArgs e)
        {
            ICompositionSellingView view = CompositionSellingView.GetInstance((MainView)_mainView);
            ICompositionSellingWithBaseRepository repository = new CompositionSellingRepository(new ApplicationContext());
            IRepository<SellViewModel> sellRepository = new SellRepository(new ApplicationContext());
            IRepository<ProductViewModel> productRepository = new ProductRepository(new ApplicationContext());
            new CompositionSellingController(view, repository, sellRepository, productRepository);
        }

        private void LoadSell(object? sender, EventArgs e)
        {
            ISellView view = SellView.GetInstance((MainView)_mainView);
            IRepository<SellViewModel> repository = new SellRepository(new ApplicationContext());
            IRepository<ShopViewModel> shopRepository = new ShopRepository(new ApplicationContext());
            new SellController(view, repository, shopRepository);
        }

        private void LoadCompositionRequest(object? sender, EventArgs e)
        {
            ICompositionRequestView view = CompositionRequestView.GetInstance((MainView)_mainView);
            IRepository<CompositionRequestViewModel> repository = new CompositionRequestRepository(new ApplicationContext());
            IRepository<ProductViewModel> productRepository = new ProductRepository(new ApplicationContext());
            IRepository<RequestViewModel> requestRepository = new RequestRepository(new ApplicationContext());
            IRepository<StorageViewModel> storageRepository = new StorageRepository(new ApplicationContext());
            new CompositionRequestController(view, repository, productRepository, requestRepository, storageRepository);
        }

        private void LoadProduct(object? sender, EventArgs e)
        {
            IProductView view = ProductView.GetInstance((MainView)_mainView);
            IRepository<ProductViewModel> repository = new ProductRepository(new ApplicationContext());
            IRepository<Product_TypeViewModel> productTypeRepository = new ProductTypeRepository(new ApplicationContext());
            IRepository<StorageViewModel> storageRepository = new StorageRepository(new ApplicationContext());
            new ProductController(view, repository, storageRepository, productTypeRepository);
        }

        private void LoadProduct_Type(object? sender, EventArgs e)
        {
            IProduct_TypeView view = Product_TypeView.GetInstance((MainView)_mainView);
            IRepository<Product_TypeViewModel> repository = new ProductTypeRepository(new ApplicationContext());
            new ProductTypeController(view, repository);
        }

        private void LoadProductIntoShop(object? sender, EventArgs e)
        {
            IProductIntoShopView view = ProductIntoShopView.GetInstance((MainView)_mainView);
            IRepository<ProductIntoShopViewModel> repository = new ProductIntoShopRepository(new ApplicationContext());
            IRepository<ShopViewModel> shopRepository = new ShopRepository(new ApplicationContext());
            IRepository<ProductViewModel> productRepository = new ProductRepository(new ApplicationContext());
            new ProductIntoShopController(view, repository, productRepository, shopRepository);
        }

        private void LoadRequest(object? sender, EventArgs e)
        {
            IRequestView view = RequestView.GetInstance((MainView)_mainView);
            IRepository<RequestViewModel> repository = new RequestRepository(new ApplicationContext());
            IRepository<ShopViewModel> shopRepository = new ShopRepository(new ApplicationContext());
            IRepository<StorageViewModel> storageRepository = new StorageRepository(new ApplicationContext());
            new RequestContorller(view, repository, storageRepository, shopRepository);
        }

        private void LoadShop(object? sender, EventArgs e)
        {
            IShopView view = ShopView.GetInstance((MainView)_mainView);
            IRepository<ShopViewModel> repository = new ShopRepository(new ApplicationContext());
            IRepository<Product_TypeViewModel> productTypeRepository = new ProductTypeRepository(new ApplicationContext());
            IRepository<ProductViewModel> productRepository = new ProductRepository(new ApplicationContext());
            new ShopController(view, repository, productTypeRepository, productRepository);
        }

        private void LoadShop_Type(object? sender, EventArgs e)
        {
            IShop_TypeView view = ShopTypeView.GetInstance((MainView)_mainView);
            IRepository<Shop_TypeViewModel> repository = new ShopTypeRepository(new ApplicationContext());
            IRepository<ShopViewModel> shopRepository = new ShopRepository(new ApplicationContext());
            IRepository<Product_TypeViewModel> protuctRepository = new ProductTypeRepository(new ApplicationContext());
            new ShopTypeController(view, repository, protuctRepository, shopRepository);
        }

        private void LoadStorage(object? sender, EventArgs e)
        {
            IStorageView view = StorageView.GetInstance((MainView)_mainView);
            IRepository<StorageViewModel> repository = new StorageRepository(new ApplicationContext());
            new StorageCotnroller(view, repository);
        }
    }

}
