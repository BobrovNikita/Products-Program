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
    public partial class RequestView : Form, IRequestView
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
        public StorageViewModel StorageId
        {
            get => (StorageViewModel)StorageCmb.SelectedItem;
            set => StorageCmb.SelectedItem = value;
        }
        public DateTime Date
        {
            get => DateObj.Value;
            set => DateObj.Value = value;
        }
        public int Product_Count
        {
            get
            {
                if (!int.TryParse(CountTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(CountTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    CountTxt.Text = value.ToString();
                }
                else
                    CountTxt.Text = string.Empty;
            }
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
        public int Nds_Sum
        {
            get
            {
                if (!int.TryParse(NDSSumTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(NDSSumTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    NDSSumTxt.Text = value.ToString();
                }
                else
                    NDSSumTxt.Text = string.Empty;
            }
        }
        public int Cost_with_NDS
        {
            get
            {
                if (!int.TryParse(NDS_Cost.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(NDS_Cost.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    NDS_Cost.Text = value.ToString();
                }
                else
                    NDS_Cost.Text = string.Empty;
            }
        }
        public int Number_Packages
        {
            get
            {
                if (!int.TryParse(NPackagesTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(NPackagesTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    NPackagesTxt.Text = value.ToString();
                }
                else
                    NPackagesTxt.Text = string.Empty;
            }
        }
        public int Weigh
        {
            get
            {
                if (!int.TryParse(WeighTxt.Text, out _))
                {
                    return 0;
                }
                else
                {
                    return int.Parse(WeighTxt.Text);
                }
            }
            set
            {
                if (value != -1)
                {
                    WeighTxt.Text = value.ToString();
                }
                else
                    WeighTxt.Text = string.Empty;
            }
        }
        public string Car
        {
            get => CarTxt.Text;
            set => CarTxt.Text = value;
        }
        public string Driver
        {
            get => DriverTxt.Text;
            set => DriverTxt.Text = value;
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
        public DateTime firstDate
        {
            get => dateTimePicker1.Value;
            set => dateTimePicker1.Value = value;
        }
        public DateTime lastDate
        {
            get => dateTimePicker2.Value;
            set => dateTimePicker2.Value = value;
        }

        public event EventHandler SearchEvent;
        public event EventHandler AddNewEvent;
        public event EventHandler EditEvent;
        public event EventHandler DeleteEvent;
        public event EventHandler SaveEvent;
        public event EventHandler CancelEvent;
        public event EventHandler SearchWithDateEvent;

        public RequestView()
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
            SearchWithDateBtn.Click += delegate { SearchWithDateEvent?.Invoke(this, EventArgs.Empty); };
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

            CountTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            NPackagesTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            CostTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            NDSSumTxt.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };

            NDS_Cost.KeyPress += (s, e) =>
            {
                if (!Char.IsDigit(e.KeyChar) && e.KeyChar != Convert.ToChar(8))
                {
                    e.Handled = true;
                }
            };
        }

        public void SetRequestBindingSource(BindingSource source)
        {
            dataGridView1.DataSource = source;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
        }

        public void SetShopBindingSource(BindingSource source)
        {
            ShopCmb.DataSource = source;
            ShopCmb.DisplayMember = "Name";
            ShopCmb.ValueMember = "Id";
        }

        public void SetStorageBindingSource(BindingSource source)
        {
            StorageCmb.DataSource = source;
            StorageCmb.DisplayMember = "Number";
            StorageCmb.ValueMember = "Id";
        }

        private static RequestView? instance;

        public static RequestView GetInstance(Form parentContainer)
        {
            if (instance == null || instance.IsDisposed)
            {
                if (parentContainer.ActiveMdiChild != null)
                    parentContainer.ActiveMdiChild.Close();

                instance = new RequestView();
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
