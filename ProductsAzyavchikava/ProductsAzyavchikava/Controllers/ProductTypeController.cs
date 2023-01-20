using ProductsAzyavchikava.Model;
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
    public class ProductTypeController
    {
        private readonly IProduct_TypeView _view;
        private readonly IRepository<Product_TypeViewModel> _repository;

        private BindingSource productTypeBindingSource;

        private IEnumerable<Product_TypeViewModel>? _product_types;

        public ProductTypeController(IProduct_TypeView view, IRepository<Product_TypeViewModel> repository)
        {
            _view = view;
            _repository = repository;

            productTypeBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;

            LoadProductTypeList();

            view.SetProductTypeBindingSource(productTypeBindingSource);

            _view.Show();
        }

        private void LoadProductTypeList()
        {
            _product_types = _repository.GetAll();
            productTypeBindingSource.DataSource = _product_types;
        }

        private void CleanViewFields()
        {
            _view.Product_Type_Id = Guid.Empty;
            _view.Product_Type_Name = string.Empty;
            _view.Product_Type_Type = string.Empty;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            var model = new Product_TypeViewModel();
            model.Id = _view.Product_Type_Id;
            model.Name = _view.Product_Type_Name;
            model.Type = _view.Product_Type_Type;
            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Product type edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Product type added successfuly";
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
                var model = (Product_TypeViewModel)productTypeBindingSource.Current;

                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Product type deleted successfuly";
                LoadProductTypeList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Product type";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (Product_TypeViewModel)productTypeBindingSource.Current;
            _view.Product_Type_Id = model.Id;
            _view.Product_Type_Name = model.Name;
            _view.Product_Type_Type = model.Type;
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
                _product_types = _repository.GetAllByValue(_view.searchValue);
            else
                _product_types = _repository.GetAll();

            productTypeBindingSource.DataSource = _product_types;
        }
    }
}
