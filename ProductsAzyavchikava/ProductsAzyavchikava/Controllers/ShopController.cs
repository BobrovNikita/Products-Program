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
    public class ShopController
    {
        private readonly IShopView _view;
        private readonly IRepository<ShopViewModel> _repository;

        private BindingSource shopBindingSource;

        private IEnumerable<ShopViewModel>? _shops;

        public ShopController(IShopView view, IRepository<ShopViewModel> repository)
        {
            _view = view;
            _repository = repository;

            shopBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadProductTypeList();

            view.SetShopBindingSource(shopBindingSource);

            _view.Show();
        }

        private void LoadProductTypeList()
        {
            _shops = _repository.GetAll();
            shopBindingSource.DataSource = _shops;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.Shop_Name = string.Empty;
            _view.Adress = string.Empty;
            _view.Area = string.Empty;
            _view.Phone = string.Empty;
            _view.Identity = -1;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            var model = new ShopViewModel();
            model.Id = _view.Id;
            model.Name = _view.Shop_Name;
            model.Adress = _view.Adress;
            model.Identity = _view.Identity;
            model.Phone = _view.Phone;
            model.Area = _view.Area;

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
                var model = (ShopViewModel)shopBindingSource.Current;

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
            var model = (ShopViewModel)shopBindingSource.Current;
            _view.Id = model.Id;
            _view.Shop_Name = model.Name;
            _view.Adress = model.Adress;
            _view.Phone = model.Phone;
            _view.Identity = model.Identity;
            _view.Area = model.Area;
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
                _shops = _repository.GetAllByValue(_view.searchValue);
            else
                _shops = _repository.GetAll();

            shopBindingSource.DataSource = _shops;
        }
    }
}
