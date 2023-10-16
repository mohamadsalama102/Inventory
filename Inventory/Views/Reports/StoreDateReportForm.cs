using Inventory.ViewModel;
using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Inventory.Views
{
    public partial class StoreDateReportForm : Form
    {
        public ProjectFullStack db = new ProjectFullStack();
        public StoreDateReportForm()
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
            public int ProductID { get; set; }
            public DateTime ExpiryDate { get; set; }
            public DateTime ProductionDate { get; set; }
            public decimal QuantityIn { get; set; } = 0;
            public decimal QuantityOut { get; set; } = 0;
            public decimal Balance => (QuantityIn) - (QuantityOut);
        }
        private void btnShow_Click(object sender, System.EventArgs e)
        {
            if (int.TryParse(textBox1.Text, out int days))
            {
                DateTime dateTime = DateTime.Now.AddDays(-days);
            
                var oldTransactions = db.StoreTransactions.Where(x => x.InsertDate < dateTime.Date).Where(x => x.StoreID == (int)comboBoxStoreID.SelectedValue)
                    .Include(x => x.ProductTransactions)
                    .SelectMany(x => x.ProductTransactions).Include(x => x.Product).Include(x => x.UnitOfMeasurement)
                    .GroupBy(x => new { x.ProductID, x.ExpiryDate, x.ProductionDate })
                    .Select(x => new TransactionView
                    {
                        ProductID = x.Key.ProductID,
                        ExpiryDate = x.Key.ExpiryDate,
                        ProductionDate = x.Key.ProductionDate,
                        QuantityIn = x.Where(s => s.InTransaction).Select(s => s.Quantity).DefaultIfEmpty().Sum(),
                        QuantityOut = x.Where(s => !s.InTransaction).Select(s => s.Quantity).DefaultIfEmpty().Sum()
                    }).ToList();

                var newTransactions = db.StoreTransactions.Where(x => x.InsertDate > dateTime.Date).Where(x => x.StoreID == (int)comboBoxStoreID.SelectedValue)
                    .SelectMany(x => x.ProductTransactions).Include(x => x.Product).Include(x => x.UnitOfMeasurement)
                    .GroupBy(x => new { x.ProductID, x.ExpiryDate, x.ProductionDate })
                    .Select(x => new TransactionView
                    {
                        ProductID = x.Key.ProductID,
                        ExpiryDate = x.Key.ExpiryDate,
                        ProductionDate = x.Key.ProductionDate,
                        QuantityIn = x.Where(s => s.InTransaction).Select(s => s.Quantity).DefaultIfEmpty().Sum(),
                        QuantityOut = x.Where(s => !s.InTransaction).Select(s => s.Quantity).DefaultIfEmpty().Sum()
                    }).ToList();
                foreach (var transaction in oldTransactions)
                {
                    var item = newTransactions
                             .Where(x => x.ProductID == transaction.ProductID)
                             .Where(x => x.ProductionDate == transaction.ProductionDate)
                             .Where(x => x.ExpiryDate == transaction.ExpiryDate)
                             .FirstOrDefault();
                    if (item != null)
                    {
                        //انقاص الكميات المنصرفة
                        transaction.QuantityOut += item.QuantityOut;
                    }
                }

                //var result = q.ToList();
                dataGridView1.DataSource = oldTransactions.ToList();
            }
        }

    }
}
