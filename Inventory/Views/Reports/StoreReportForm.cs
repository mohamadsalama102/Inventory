using Inventory.ViewModel;
using System;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Inventory.Views
{
    public partial class StoreReportForm : Form
    {
        public ProjectFullStack db = new ProjectFullStack();
        public StoreReportForm()
        {
            InitializeComponent();
            comboBoxStoreID.DataSource = db.Stores.ToList();
            comboBoxStoreID.DisplayMember = nameof(Store.StoreName);
            comboBoxStoreID.ValueMember = nameof(Store.StoreID);
            dataGridView1.AutoGenerateColumns = false;

            AddColumns(dataGridView1, "ProductName", "اسم الصنف", DataGridViewAutoSizeColumnMode.Fill);
            AddColumns(dataGridView1, "UnitName", "الوحدة", DataGridViewAutoSizeColumnMode.NotSet);
            AddColumns(dataGridView1, "Quantity", "الكمية", DataGridViewAutoSizeColumnMode.NotSet);
            AddColumns(dataGridView1, "Price", "السعر", DataGridViewAutoSizeColumnMode.NotSet);
            AddColumns(dataGridView1, "Total", "الاجمالي", DataGridViewAutoSizeColumnMode.NotSet);
            AddColumns(dataGridView1, "State", "الحالة", DataGridViewAutoSizeColumnMode.NotSet);

            dataGridView2.AutoGenerateColumns = false;
            AddColumns(dataGridView2, "ProductName", "اسم الصنف", DataGridViewAutoSizeColumnMode.Fill);
            AddColumns(dataGridView2, "UnitName", "الوحدة", DataGridViewAutoSizeColumnMode.NotSet);
            AddColumns(dataGridView2, "Quantity", "الكمية", DataGridViewAutoSizeColumnMode.NotSet);
            AddColumns(dataGridView2, "Price", "السعر", DataGridViewAutoSizeColumnMode.NotSet);
            AddColumns(dataGridView2, "Total", "الاجمالي", DataGridViewAutoSizeColumnMode.NotSet);
            AddColumns(dataGridView2, "State", "الحالة", DataGridViewAutoSizeColumnMode.DisplayedCells);

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
        private void btnShow_Click(object sender, System.EventArgs e)
        {
            StoreTransactions(dateFrom.Value, dateTo.Value, (int)comboBoxStoreID.SelectedValue);
            StoreTransfers(dateFrom.Value, dateTo.Value, (int)comboBoxStoreID.SelectedValue);

        }
        void StoreTransactions(DateTime dateFrom, DateTime dateTo, int StoreID)
        {
            var q = db.StoreTransactions.AsQueryable();
            q = q.Where(x => x.InsertDate >= dateFrom)
            .Where(x => x.InsertDate <= dateTo)
            .Where(x => x.StoreID == StoreID);
            var result = q.SelectMany(x => x.ProductTransactions).Include(x => x.Product).Include(x => x.UnitOfMeasurement).Select(x => new
            {
                x.Product.ProductName,
                x.UnitOfMeasurement.UnitName,
                x.Quantity,
                x.Price,
                Total = x.Quantity * x.Price,
                State = x.InTransaction ? "وارد" : "صادر"
            });
            dataGridView1.DataSource = result.ToList();
        }
        void StoreTransfers(DateTime dateFrom, DateTime dateTo, int StoreID)
        {
            var q = db.StoreTransfers.AsQueryable();
            q = q.Where(x => x.InsertDate >= dateFrom)
            .Where(x => x.InsertDate <= dateTo)
            .Where(x => x.StoreFromID == StoreID || x.StoreToID == StoreID);
            var result = q.SelectMany(x => x.ProductTransfers)
                .Where(x => x.StoreID == StoreID)
                .Include(x => x.Product)
                .Include(x => x.UnitOfMeasurement)
               // .Include(x => x.StoreTransfer)
                .Select(x => new
                {
                    x.Product.ProductName,
                    x.UnitOfMeasurement.UnitName,
                    x.Quantity,
                    x.Price,
                    Total = x.Quantity * x.Price,
                    State = x.InTransaction ? "محول من:" + x.StoreTransfer.StoreFrom.StoreName : "تحويل الي:" + x.StoreTransfer.StoreTo.StoreName,
                    x.StoreTransfer.StoreFrom.StoreName
                });
            dataGridView2.DataSource = result.ToList();
        }

    }
}
