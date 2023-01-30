using Microsoft.Office.Interop.Word;
using ProductsAzyavchikava.Model;
using ProductsAzyavchikava.Repositories;
using ProductsAzyavchikava.Views.Intefraces;
using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;

namespace ProductsAzyavchikava.Controllers
{
    public class CompositionRequestController
    {
        private readonly ICompositionRequestView _view;
        private readonly IRepository<CompositionRequestViewModel> _repository;
        private readonly IRepository<ProductViewModel> _productRepository;
        private readonly IRepository<RequestViewModel> _requestRepository;
        private readonly IRepository<StorageViewModel> _storageRepository;

        private BindingSource compositionRequestBindingSource;
        private BindingSource ProductBindingSource;
        private BindingSource RequestBindingSource;

        private IEnumerable<RequestViewModel>? _requests;
        private IEnumerable<ProductViewModel>? _products;
        private IEnumerable<CompositionRequestViewModel>? _composition;

        public CompositionRequestController(ICompositionRequestView view, IRepository<CompositionRequestViewModel> repository, IRepository<ProductViewModel> productRepository, IRepository<RequestViewModel> requestRepository, IRepository<StorageViewModel> storageRepository)
        {
            _view = view;
            _repository = repository;
            _productRepository = productRepository;
            _requestRepository = requestRepository;
            _storageRepository = storageRepository;

            compositionRequestBindingSource = new BindingSource();
            ProductBindingSource = new BindingSource();
            RequestBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.RemainingStockEvent += RemainingStock;

            LoadProductTypeList();
            LoadCombobox();

            view.SetCompositionBindingSource(compositionRequestBindingSource);
            view.SetRequestBindingSource(RequestBindingSource);
            view.SetProductBindingSource(ProductBindingSource);

            _view.Show();
        }

        private void LoadProductTypeList()
        {
            _composition = _repository.GetAll();
            compositionRequestBindingSource.DataSource = _composition;
        }

        private void LoadCombobox()
        {
            _products = _productRepository.GetAll();
            ProductBindingSource.DataSource = _products;

            _requests = _requestRepository.GetAll();
            RequestBindingSource.DataSource = _requests;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.RequestId = new RequestViewModel();
            _view.ProductId = new ProductViewModel();
            _view.Count = -1;
            _view.Sum = -1;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.RequestId == null || _view.ProductId == null)
            {
                CleanViewFields();
                _view.Message = "Values are not specified in the combobox";
                return;
            }

            var model = new CompositionRequestViewModel();
            model.Id = _view.Id;
            model.RequestId = _view.RequestId.Id;
            model.ProductId = _view.ProductId.ProductId;
            model.Sum = _view.Sum;
            model.Count = _view.Count;
            model.ProductVenderCode = _view.ProductId.VendorCode;
            model.ProductCount = _view.RequestId.Products_Count;
            model.Date = _view.RequestId.Date;
            model.ProductName = _view.ProductId.PName;

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Composition edited successfuly";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Composition added successfuly";
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
                var model = (CompositionRequestViewModel)compositionRequestBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Composition deleted successfuly";
                LoadProductTypeList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Composition";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (CompositionRequestViewModel)compositionRequestBindingSource.Current;
            _view.Id = model.Id;
            _view.RequestId.Id = model.RequestId;
            _view.ProductId.ProductId = model.ProductId;
            _view.Count = model.Count;
            _view.Sum = model.Sum;
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
                _composition = _repository.GetAllByValue(_view.searchValue);
            else
                _composition = _repository.GetAll();

            compositionRequestBindingSource.DataSource = _composition;
        }


        private void RemainingStock(object? sender, EventArgs e)
        {
            var viewModel = (CompositionRequestViewModel)compositionRequestBindingSource.Current;
            var productViewModel = _productRepository.GetModel(viewModel.ProductId);
            var storageViewModel = _storageRepository.GetModel(productViewModel.StorageId);

            var compositionRequestsList = _repository.GetAllByValue(viewModel.Id.ToString());
            var productList = compositionRequestsList.Select(p => _productRepository.GetModel(p.ProductId)).ToList();


            

            Word.Application wApp = new Word.Application();
            wApp.Visible = true;
            object missing = Type.Missing;
            object falseValue = false;
            Word.Document wordDocument = wApp.Documents.Open(Path.Combine(System.Windows.Forms.Application.StartupPath, Directory.GetCurrentDirectory() + "\\ОстатокНаСкладе.docx"));
            ReplaceWordStub("{Adress}", storageViewModel.Adress, wordDocument);
            ReplaceWordStub("{dateNow}", DateTime.Now.ToShortDateString(), wordDocument);
            Word.Table tb = wordDocument.Tables[1];
            foreach (var rw in productList)
            {
                Word.Row r = tb.Rows.Add();
                r.Cells[1].Range.Text = rw.PName.Trim();
                r.Cells[2].Range.Text = rw.Retail_Price.ToString();
                r.Cells[3].Range.Text = rw.Cost.ToString();
                r.Cells[4].Range.Text = rw.Availability.ToString();

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
