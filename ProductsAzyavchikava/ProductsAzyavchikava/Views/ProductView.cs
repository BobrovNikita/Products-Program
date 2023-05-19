using ProductsAzyavchikava.Views.Intefraces;
using ProductsAzyavchikava.Views.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductsAzyavchikava.Views
{
    public partial class ProductView : Form, IProductView
    {
        private string? _message;
        private bool _isSuccessful;
        private bool _isEdit;

        public Guid Id
        {
            get => Guid.Parse(IdTxt.Text);
            set => IdTxt.Text = value.ToString();
        }
        public Product_TypeViewModel ProductTypeId
        {
            get => (Product_TypeViewModel)ProductTypeTxb.SelectedItem;
            set => ProductTypeTxb.SelectedItem = value;
        }
        public StorageViewModel StorageId
        {
            get => (StorageViewModel)StorageCmb.SelectedItem;
            set => StorageCmb.SelectedItem = value;
        }

        public string PName
        {
            get => PNameTxt.Text; 
            set => PNameTxt.Text = value;
        }
        public string VendorCode
        {
            get => VendorCodeTxt.Text;
            set => VendorCodeTxt.Text = value;
        }
        public string Hatch
        {
            get => HatchTxt.Text;
            set => HatchTxt.Text = value;
        }
        public int Cost
        {
            get
            {
                if (!int.TryParse(CostTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(CostTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    CostTxt.Text = value.ToString();
                }
                else
                    CostTxt.Text = string.Empty;
            }
        }
        public int NDS
        {
            get
            {
                if (!int.TryParse(NDSTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(NDSTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    NDSTxt.Text = value.ToString();
                }
                else
                    NDSTxt.Text = string.Empty;
            }
        }
        public int Markup
        {
            get
            {
                if (!int.TryParse(MarkupTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(MarkupTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    MarkupTxt.Text = value.ToString();
                }
                else
                    MarkupTxt.Text = string.Empty;
            }
        }
        public string Production
        {
            get => ProductionTxt.Text;
            set => ProductionTxt.Text = value;
        }
        public int Weight_Per_Price
        {
            get
            {
                if (!int.TryParse(Weight_Per_PriceTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(Weight_Per_PriceTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    Weight_Per_PriceTxt.Text = value.ToString();
                }
                else
                    Weight_Per_PriceTxt.Text = string.Empty;
            }
        }
        public int Weight
        {
            get
            {
                if (!int.TryParse(WeightTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(WeightTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    WeightTxt.Text = value.ToString();
                }
                else
                    WeightTxt.Text = string.Empty;
            }
        }
        public bool Availability
        {
            get => AvailabilityTxt.Checked;
            set => AvailabilityTxt.Checked = value;
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

        public ProductView()
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
                    MessageBox.Show("You didn't choose some redord");
                }
            };

            //Delete
            DeleteBtn.Click += delegate
            {
                var result = MessageBox.Show("Are you sure you want to delete the selected record", "Warning",
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

            CostTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            NDSTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            MarkupTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            Weight_Per_PriceTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            AvailabilityTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };
        }


        public void SetProductBindingSource(BindingSource source)
        {
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
        }

        public void SetProductTypeBindingSource(BindingSource source)
        {
            ProductTypeTxb.DataSource = source;
            ProductTypeTxb.DisplayMember = "Type";
            ProductTypeTxb.ValueMember = "Id";
        }

        public void SetStorageBindingSource(BindingSource source)
        {
            StorageCmb.DataSource = source;
            StorageCmb.DisplayMember = "Number";
            StorageCmb.ValueMember = "Id";
        }

        private static ProductView? instance;

        public static ProductView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                if (parentContainer.ActiveMdiChild != null)
                    parentContainer.ActiveMdiChild.Close();

                instance = new ProductView();
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
