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
    public class SellController
    {
        private readonly ISellView _view;
        private readonly IRepository<SellViewModel> _repository;
        private readonly IRepository<ShopViewModel> _shopRepository;

        private BindingSource sellBindingSource;
        private BindingSource shopBindingSource;

        private IEnumerable<SellViewModel>? _sells;
        private IEnumerable<ShopViewModel>? _shops;

        public SellController(ISellView view, IRepository<SellViewModel> repository, IRepository<ShopViewModel> shopRepository)
        {
            _view = view;
            _repository = repository;
            _shopRepository = shopRepository;

            sellBindingSource = new BindingSource();
            shopBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadSellsList();
            LoadCombobox();

            view.SetSellBindingSource(sellBindingSource);
            view.SetShopBindingSource(shopBindingSource);

            _view.Show();
        }

        private void LoadSellsList()
        {
            _sells = _repository.GetAll();
            sellBindingSource.DataSource = _sells;
        }

        private void LoadCombobox()
        {
            _shops = _shopRepository.GetAll();
            shopBindingSource.DataSource = _shops;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.ShopId = new ShopViewModel();
            _view.PaymentMethod = "Наличные";
            _view.Date = DateTime.Now;
            _view.FIOSalesman = string.Empty;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.ShopId == null)
            {
                CleanViewFields();
                _view.Message = "Нет значения в выпадающем списке";
                return;
            }

            var model = new SellViewModel();
            model.SellId = _view.Id;
            model.ShopId = _view.ShopId.Id;
            model.Date = _view.Date;
            model.FIOSalesman = _view.FIOSalesman;
            model.PaymentMethod = _view.PaymentMethod;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Продажа отредактирована успешно";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Продажа добавлена успешно";
                }
                _view.IsSuccessful = true;
                LoadSellsList();
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
                var model = (SellViewModel)sellBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Продажа удалена успешно";
                LoadSellsList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "Произошла ошибка, не удалось удалить запись";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (SellViewModel)sellBindingSource.Current;
            _view.Id = model.SellId;
            _view.ShopId.Id = model.ShopId;
            _view.Date = model.Date;
            _view.PaymentMethod = model.PaymentMethod;
            _view.FIOSalesman = model.FIOSalesman;
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
                _sells = _repository.GetAllByValue(_view.searchValue);
            else
                _sells = _repository.GetAll();

            sellBindingSource.DataSource = _sells;
        }
    }
}
