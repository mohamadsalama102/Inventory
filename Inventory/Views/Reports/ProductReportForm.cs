using Inventory.ViewModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows.Forms;

namespace Inventory.Views
{
    public partial class ProductReportForm : Form
    {
        public ProjectFullStack db = new ProjectFullStack();
        public ProductReportForm()
        {
            InitializeComponent();
            checkedListBoxStores.DataSource = db.Stores.ToList();
            checkedListBoxStores.DisplayMember = nameof(Store.StoreName);
            checkedListBoxStores.ValueMember = nameof(Store.StoreID);

            comboBoxProductID.DataSource = db.Products.ToList();
            comboBoxProductID.DisplayMember = nameof(Product.ProductName);
            comboBoxProductID.ValueMember = nameof(Product.ProductID);
            dataGridView1.AutoGenerateColumns = false;

            AddColumns(dataGridView1, "StoreName", "اسم المخزن", DataGridViewAutoSizeColumnMode.Fill);
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
        decimal QuantityIn, QuantityOut;
        private void btnShow_Click(object sender, System.EventArgs e)
        {
            List<int> storeIds = new List<int>();
            foreach (var itemChecked in checkedListBoxStores.CheckedItems)
                if (itemChecked is Store store) storeIds.Add(store.StoreID);

            QuantityIn = QuantityOut = 0;

            StoreTransactions(dateFrom.Value, dateTo.Value, (int)comboBoxProductID.SelectedValue, storeIds.ToArray());
            StoreTransfers(dateFrom.Value, dateTo.Value, (int)comboBoxProductID.SelectedValue, storeIds.ToArray());
            qtyIn.Text = QuantityIn.ToString();
            qtyOut.Text = QuantityOut.ToString();
            QuantityBalance.Text = (QuantityIn - QuantityOut).ToString();
        }
        void StoreTransactions(DateTime dateFrom, DateTime dateTo, int ProductID, int[] StoreIDs)
        {
            var q = db.StoreTransactions.AsQueryable();
            q = q.Where(x => x.InsertDate >= dateFrom).Where(x => x.InsertDate <= dateTo)
            .Where(x => StoreIDs.Contains(x.StoreID));

            var result = q.SelectMany(x => x.ProductTransactions)
                .Where(x => x.ProductID == ProductID)
                .Include(x => x.Product)
                .Include(x => x.UnitOfMeasurement)
                .Select(x => new
                {
                    x.Store.StoreName,
                    x.Product.ProductName,
                    x.UnitOfMeasurement.UnitName,
                    x.Quantity,
                    x.Price,
                    Total = x.Quantity * x.Price,
                    x.InTransaction,
                    State = x.InTransaction ? "وارد" : "صادر"
                }).ToList();
            QuantityIn = +result.Where(x => x.InTransaction).Sum(x => x.Quantity);
            QuantityOut = +result.Where(x => !x.InTransaction).Sum(x => x.Quantity);
            dataGridView1.DataSource = result;
        }
        void StoreTransfers(DateTime dateFrom, DateTime dateTo, int ProductID, int[] StoreIDs)
        {
            var q = db.StoreTransfers.AsQueryable();
            q = q.Where(x => x.InsertDate >= dateFrom)
            .Where(x => x.InsertDate <= dateTo);
            var result = q.SelectMany(x => x.ProductTransfers)
                .Where(x => x.ProductID == ProductID)
                .Where(x => StoreIDs.Contains(x.StoreID))
                .Include(x => x.Product)
                .Include(x => x.UnitOfMeasurement)
                .Select(x => new
                {
                    x.Product.ProductName,
                    x.UnitOfMeasurement.UnitName,
                    x.Quantity,
                    x.Price,
                    x.InTransaction,
                    Total = x.Quantity * x.Price,
                    x.State,
                }).ToList();
            QuantityIn += result.Where(x => x.InTransaction).Sum(x => x.Quantity);
            QuantityOut += result.Where(x => !x.InTransaction).Sum(x => x.Quantity);

            dataGridView2.DataSource = result;
        }

    }
}
