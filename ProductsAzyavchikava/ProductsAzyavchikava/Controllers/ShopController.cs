using Microsoft.Office.Interop.Word;
using ProductsAzyavchikava.Model;
using ProductsAzyavchikava.Repositories;
using ProductsAzyavchikava.Views.Intefraces;
using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace ProductsAzyavchikava.Controllers
{
    public class ShopController
    {
        private readonly IShopView _view;
        private readonly IRepository<ShopViewModel> _repository;
        private readonly IRepository<Product_TypeViewModel> _productTypeRepository;
        private readonly IRepository<ProductViewModel> _productRepository;

        private BindingSource shopBindingSource;

        private IEnumerable<ShopViewModel>? _shops;

        public ShopController(IShopView view, IRepository<ShopViewModel> repository, IRepository<Product_TypeViewModel> productTypeRepository, IRepository<ProductViewModel> productRepository)
        {
            _view = view;
            _repository = repository;
            _productTypeRepository = productTypeRepository;
            _productRepository = productRepository;

            shopBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.PrintEvent += Print;

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


        private void Print(object? sender, EventArgs e)
        {
            var viewModel = (ShopViewModel)shopBindingSource.Current;



            var pType = _productTypeRepository.GetAll();

            var products = _productRepository.GetAll();

            var temp = products
                    .GroupBy(p => p.Product_TypeId)
                    .Select(g => new
                    {
                        Name = g.Key,
                        Employees = g.Select(p => p),
                        Count = g.Count(),
                    }).ToList();

            var result = pType.Join(temp,
                                    p => p.Id,
                                    pr => pr.Name,
                                    (p, pr) => new
                                    {
                                        Name = p.Name,
                                        Count = pr.Count,
                                    }).ToList();

            Word.Application wApp = new Word.Application();
            wApp.Visible = true;
            object missing = Type.Missing;
            object falseValue = false;
            Word.Document wordDocument = wApp.Documents.Open(Path.Combine(System.Windows.Forms.Application.StartupPath, Directory.GetCurrentDirectory() + "\\ЗаявлениеОСоглосовТовар.docx"));
            ReplaceWordStub("{ShopName}", viewModel.Name, wordDocument);
            ReplaceWordStub("{Area}", viewModel.Area, wordDocument);
            ReplaceWordStub("{Area}", viewModel.Area, wordDocument);
            ReplaceWordStub("{Adress}", viewModel.Adress, wordDocument);
            Word.Table tb = wordDocument.Tables[2];
            foreach (var rw in result)
            {
                Word.Row r = tb.Rows.Add();
                r.Cells[1].Range.Text = rw.Name.Trim();
                r.Cells[2].Range.Text = rw.Count.ToString();

            }
            tb.Rows[2].Delete(); // удаляем пустую строку после шапки таблицы
        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocumet)
        {
            var range = wordDocumet.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }
    }
}
