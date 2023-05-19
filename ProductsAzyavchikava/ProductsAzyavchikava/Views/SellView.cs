using ProductsAzyavchikava.Views.Intefraces;
using ProductsAzyavchikava.Views.ViewModels;

namespace ProductsAzyavchikava.Views
{
    public partial class SellView : Form, ISellView
    {
        private string? _message;
        private bool _isSuccessful;
        private bool _isEdit;

        public Guid Id
        {
            get => Guid.Parse(IdTxt.Text);
            set => IdTxt.Text = value.ToString();
        }
        public ShopViewModel ShopId
        {
            get => (ShopViewModel)ShopCmb.SelectedItem;
            set => ShopCmb.SelectedItem = value;
        }
        public DateTime Date
        {
            get => DateObj.Value;
            set => DateObj.Value = value;
        }
        public string PaymentMethod
        {
            get => PaymentMethodCmb.SelectedItem.ToString();
            set => PaymentMethodCmb.SelectedItem = value.ToString();
        }
        public string FIOSalesman
        {
            get => FIOSalesmanTxt.Text; 
            set => FIOSalesmanTxt.Text = value;
        }
        public string searchValue
        {
            get => SearchTxb.Text;
            set => SearchTxb.Text = value;
        }
        public bool IsEdit
        {
            get => _isEdit;
            set => _isEdit = value;
        }
        public bool IsSuccessful
        {
            get => _isSuccessful;
            set => _isSuccessful = value;
        }
        public string Message
        {
            get => _message;
            set => _message = value;
        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;

        public SellView()
        {
            InitializeComponent();
            AssosiateAndRaiseViewEvents();
            tabControl1.TabPages.Remove(tabPage2);
            CloseBtn.Click += delegate { this.Close(); };
            IdTxt.Text = Guid.Empty.ToString();
        }

        private void AssosiateAndRaiseViewEvents()
        {
            //Search
            SearchBtn.Click += delegate { SearchEvent?.Invoke(this, EventArgs.Empty); };
            SearchTxb.KeyDown += (s, e) =>
            {
                if (e.KeyData == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    SearchEvent?.Invoke(this, EventArgs.Empty);
                }
            };

            //Add new
            AddBtn.Click += delegate
            {
                AddNewEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(tabPage2);
                tabControl1.TabPages.Remove(tabPage1);
                tabPage2.Text = "Добавление";
            };

            //Edit
            EditBtn.Click += delegate
            {
                if (dataGridView1.Rows.Count >= 1)
                {
                    tabControl1.TabPages.Remove(tabPage1);
                    tabControl1.TabPages.Add(tabPage2);
                    EditEvent?.Invoke(this, EventArgs.Empty);
                    tabPage2.Text = "Редактирование";
                }
                else
                {
                    MessageBox.Show("Вы не выбрали запись");
                }
            };

            //Delete
            DeleteBtn.Click += delegate
            {
                var result = MessageBox.Show("Вы уверены что хотите удалить выбранную запись?", "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    DeleteEvent?.Invoke(this, EventArgs.Empty);
                    MessageBox.Show(Message);
                }
            };

            //Save 
            SaveBtn.Click += delegate
            {
                SaveEvent?.Invoke(this, EventArgs.Empty);
                if (IsSuccessful)
                {
                    tabControl1.TabPages.Add(tabPage1);
                    tabControl1.TabPages.Remove(tabPage2);
                }

                MessageBox.Show(Message);
            };

            //Cancel
            CancelBtn.Click += delegate
            {
                CancelEvent?.Invoke(this, EventArgs.Empty);
                tabControl1.TabPages.Add(tabPage1);
                tabControl1.TabPages.Remove(tabPage2);
            };
        }

        public void SetSellBindingSource(BindingSource source)
        {
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
        }

        public void SetShopBindingSource(BindingSource source)
        {
            ShopCmb.DataSource = source;
            ShopCmb.DisplayMember = "Name";
            ShopCmb.ValueMember = "Id";
        }

        private static SellView? instance;

        public static SellView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                if (parentContainer.ActiveMdiChild != null)
                    parentContainer.ActiveMdiChild.Close();

                instance = new SellView();
                instance.MdiParent = parentContainer;
                instance.FormBorderStyle = FormBorderStyle.None;
                instance.Dock = DockStyle.Fill;
            }
            else
            {
                if (instance.WindowState == FormWindowState.Minimized)
                    instance.WindowState = FormWindowState.Normal;

                instance.BringToFront();
            }

            return instance;
        }
    }
}
