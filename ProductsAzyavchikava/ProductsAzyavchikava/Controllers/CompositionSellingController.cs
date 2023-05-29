using ProductsAzyavchikava.Repositories;
using ProductsAzyavchikava.Views.Intefraces;
using ProductsAzyavchikava.Views.ViewModels;
using Word = Microsoft.Office.Interop.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductsAzyavchikava.Views;

namespace ProductsAzyavchikava.Controllers
{
    public class CompositionSellingController
    {
        private readonly ICompositionSellingView _view;
        private readonly IMainView _mainView;
        private readonly ICompositionSellingWithBaseRepository _repository;
        private readonly IRepository<SellViewModel> _sellRepository;
        private readonly IRepository<ProductViewModel> _productRepository;

        private BindingSource compositionSellBindingSource;
        private BindingSource sellBindingSource;
        private BindingSource productBindingSource;

        private IEnumerable<CompositionSellingViewModel>? _compositionSelling;
        private IEnumerable<SellViewModel>? _sell;
        private IEnumerable<ProductViewModel>? _product;

        public CompositionSellingController(ICompositionSellingView view, ICompositionSellingWithBaseRepository repository, IRepository<SellViewModel> sellRepository, IRepository<ProductViewModel> productRepository, IMainView mainView)
        {
            _view = view;
            _repository = repository;
            _sellRepository = sellRepository;
            _productRepository = productRepository;
            _mainView = mainView;

            compositionSellBindingSource = new BindingSource();
            sellBindingSource = new BindingSource();
            productBindingSource = new BindingSource();

            view.SearchEvent += Search;
            view.AddNewEvent += Add;
            view.EditEvent += LoadSelectedToEdit;
            view.DeleteEvent += DeleteSelected;
            view.SaveEvent += Save;
            view.CancelEvent += CancelAction;
            view.CheckPrintEvent += CheckPrint;
            view.SellOpen += SellOpen;

            LoadCompositionSellingList();
            LoadCombobox();

            view.SetCompositionSellingBindingSource(compositionSellBindingSource);
            view.SetProductBindingSource(productBindingSource);
            view.SetSellBindingSource(sellBindingSource);

            _view.Show();
        }

        private void SellOpen(object? sender, EventArgs e)
        {
            ISellView view = SellView.GetInstance((MainView)_mainView);
            IRepository<SellViewModel> repository = new SellRepository(new ApplicationContext());
            IRepository<ShopViewModel> shopRepository = new ShopRepository(new ApplicationContext());
            new SellController(view, repository, shopRepository, _mainView);
        }

        private void CheckPrint(object? sender, EventArgs e)
        {
            var viewModel = (CompositionSellingViewModel)compositionSellBindingSource.Current;
            var viewModels = _repository.GettAllById(viewModel.SellId);
            var sellModel = _sellRepository.GetModel(viewModel.SellId);
            var sum = viewModels.Sum(s => s.Sum);

            Word.Application wApp = new Word.Application();
            wApp.Visible = true;
            object missing = Type.Missing;
            object falseValue = false;
            Word.Document wordDocument = wApp.Documents.Open(Path.Combine(System.Windows.Forms.Application.StartupPath, Directory.GetCurrentDirectory() + "\\Чек.docx"));
            ReplaceWordStub("{DateSell}", viewModel.SellDate.ToString(), wordDocument);
            ReplaceWordStub("{Sum}", sum.ToString(), wordDocument);
            ReplaceWordStub("{Sum}", sum.ToString(), wordDocument);
            ReplaceWordStub("{PayMethod}", sellModel.PaymentMethod.ToString(), wordDocument);
            Word.Table tb = wordDocument.Tables[1];
            foreach (var rw in viewModels)
            {
                Word.Row r = tb.Rows.Add();
                r.Cells[1].Range.Text = rw.ProductName.Trim();
                r.Cells[2].Range.Text = rw.Count.ToString() + "   = ";
                r.Cells[3].Range.Text = (rw.ProductCost*rw.Count).ToString();
            }
            tb.Rows[1].Delete(); // удаляем пустую строку после шапки таблицы
        }

        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocumet)
        {
            var range = wordDocumet.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
        }

        private void LoadCompositionSellingList()
        {
            _compositionSelling = _repository.GetAll();
            compositionSellBindingSource.DataSource = _compositionSelling;
        }

        private void LoadCombobox()
        {
            _product = _productRepository.GetAll();
            productBindingSource.DataSource = _product;

            _sell = _sellRepository.GetAll();
            sellBindingSource.DataSource = _sell;
        }

        private void CleanViewFields()
        {
            _view.Id = Guid.Empty;
            _view.SellId = new SellViewModel();
            _view.ProductId = new ProductViewModel();
            _view.Count = -1;
        }

        private void CancelAction(object? sender, EventArgs e)
        {
            CleanViewFields();
        }

        private void Save(object? sender, EventArgs e)
        {
            if (_view.SellId == null || _view.ProductId == null)
            {
                CleanViewFields();
                _view.Message = "Нет значения в выпадающем списке";
                return;
            }

            var model = new CompositionSellingViewModel();
            model.Id = _view.Id;
            model.SellId = _view.SellId.SellId;
            model.ProductId = _view.ProductId.ProductId;
            model.Count = _view.Count;

            if (!_productRepository.GetModel(model.ProductId).Availability)
            {
                _view.Message = "Товара нет в наличии";
                return;
            }

            try
            {
                if (_view.IsEdit)
                {
                    _repository.Update(model);
                    _view.Message = "Состав продажи изменена";
                }
                else
                {
                    _repository.Create(model);
                    _view.Message = "Состав продажи добавлен";
                }
                _view.IsSuccessful = true;
                LoadCompositionSellingList();
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
                var model = (CompositionSellingViewModel)compositionSellBindingSource.Current;
                if (model == null)
                {
                    throw new Exception();
                }
                _repository.Delete(model);
                _view.IsSuccessful = true;
                _view.Message = "Состав продажи удален";
                LoadCompositionSellingList();
            }
            catch (Exception)
            {
                _view.IsSuccessful = false;
                _view.Message = "An error ocurred, could not delete Request";
            }
        }

        private void LoadSelectedToEdit(object? sender, EventArgs e)
        {
            var model = (CompositionSellingViewModel)compositionSellBindingSource.Current;
            _view.Id = model.Id;
            _view.ProductId.ProductId = model.ProductId;
            _view.SellId.SellId = model.SellId;
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
                _compositionSelling = _repository.GetAllByValue(_view.searchValue);
            else
                _compositionSelling = _repository.GetAll();

            compositionSellBindingSource.DataSource = _compositionSelling;
        }
    }
}
