using Inventory.ViewModel;
using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Inventory.Views
{
    public partial class ExpiryDateReportForm : Form
    {
        public ProjectFullStack db = new ProjectFullStack();
        public ExpiryDateReportForm()
        {
            InitializeComponent();
            comboBoxStoreID.DataSource = db.Stores.ToList();
            comboBoxStoreID.DisplayMember = nameof(Store.StoreName);
            comboBoxStoreID.ValueMember = nameof(Store.StoreID);
            //dataGridView1.AutoGenerateColumns = false;

            //AddColumns(dataGridView1, "ProductName", "اسم الصنف", DataGridViewAutoSizeColumnMode.Fill);
            //AddColumns(dataGridView1, "UnitName", "الوحدة", DataGridViewAutoSizeColumnMode.NotSet);
            //AddColumns(dataGridView1, "Quantity", "الكمية", DataGridViewAutoSizeColumnMode.NotSet);
            //AddColumns(dataGridView1, "Price", "السعر", DataGridViewAutoSizeColumnMode.NotSet);
            //AddColumns(dataGridView1, "Total", "الاجمالي", DataGridViewAutoSizeColumnMode.NotSet);
            //AddColumns(dataGridView1, "State", "الحالة", DataGridViewAutoSizeColumnMode.NotSet);


        }
        void AddColumns(DataGridView view, string col, string header, DataGridViewAutoSizeColumnMode mode)
        {
            DataGridViewColumn column = new DataGridViewColumn();
            column.Name = $"column{col}";
            column.DataPropertyName = col;
            column.HeaderText = header;
            column.CellTemplate = new DataGridViewTextBoxCell();
            column.AutoSizeMode = mode;
            view.Columns.Add(column);

        }
        class TransactionView
        {
            public string ProductName { get; set; }
            public DateTime ExpiryDate { get; set; }
            public int TotalDays => (int)ExpiryDate.Subtract(DateTime.Now).TotalDays;
            public decimal QuantityIn {private get; set; } = 0;
            public decimal QuantityOut { private get; set; } = 0;
            public decimal Balance => (QuantityIn) - (QuantityOut);
        }
        private void btnShow_Click(object sender, System.EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int days))
            {
                DateTime dateTime = DateTime.Now.AddDays(days);
            
                var oldTransactions = db.StoreProducts
                    .Where(x => x.StoreID == (int)comboBoxStoreID.SelectedValue)
                    .Where(x => x.ExpiryDate <= dateTime.Date)
                    .Include(x => x.Product)
                    .Include(x => x.UnitOfMeasurement)
                    .GroupBy(x => new { x.ProductID, x.ExpiryDate })
                    .Select(x => new TransactionView
                    {
                        ProductName = x.FirstOrDefault().Product.ProductName,
                        ExpiryDate = x.Key.ExpiryDate,
                        QuantityIn = x.Where(s => s.InTransaction).Select(s => s.Quantity).DefaultIfEmpty().Sum(),
                        QuantityOut = x.Where(s => !s.InTransaction).Select(s => s.Quantity).DefaultIfEmpty().Sum()
                    }).ToList();
                //var result = q.ToList();
                dataGridView1.DataSource = oldTransactions.ToList();
            }
        }

    }
}
