using Inventory.Entitys;
using Inventory.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory.Views
{

    public partial class StoreTransactionForm : CRUDForm
    {
        internal IViewModel<StoreTransaction> storeViewModel;

        public TransactionType Type { get; }

        public StoreTransactionForm(TransactionType type)
        {
            InitializeComponent();
            storeViewModel = new StoreTransactionViewModel();

            Type = type;

            InitializeEvent();
            InitializeData();

        }
        void InitializeEvent()
        {
            this.comboBoxProducts.SelectedIndexChanged += comboBoxProducts_SelectedIndexChanged;
            this.comboBoxUnits.SelectedIndexChanged += ComboBoxUnits_SelectedIndexChanged;
            dgvItems.CellFormatting += DgvItems_CellFormatting; ;
            dgvItems.DoubleClick += DgvItems_DoubleClick;
        }


        List<Product> ProductList;
        List<UnitOfMeasurement> UnitList;
        void InitializeData()
        {
            switch (Type)
            {
                case TransactionType.TransactionIn:
                    Text = "اذن توريد";
                    lblPersonType.Text = "مورد";
                    comboBoxPersonID.DataSource = (storeViewModel as StoreTransactionViewModel).GetSuppliers();
                    comboBoxPersonID.DisplayMember = nameof(Supplier.SupplierName);
                    comboBoxPersonID.ValueMember = nameof(Supplier.SupplierID);


                    break;
                case TransactionType.TransactionOut:
                    Text = "اذن صرف";
                    lblPersonType.Text = "عميل";
                    comboBoxPersonID.DataSource = (storeViewModel as StoreTransactionViewModel).GetCustomers();
                    comboBoxPersonID.DisplayMember = nameof(Customer.CustomerName);
                    comboBoxPersonID.ValueMember = nameof(Customer.CustomerID);
                    break;
            }
            comboBoxStoreID.DataSource = (storeViewModel as StoreTransactionViewModel).GetStores();
            comboBoxStoreID.DisplayMember = nameof(Store.StoreName);
            comboBoxStoreID.ValueMember = nameof(Store.StoreID);

            UnitList = (storeViewModel as StoreTransactionViewModel).GetUnits(-1);
            ProductList = (storeViewModel as StoreTransactionViewModel).GetProducts();
            comboBoxProducts.DataSource = ProductList;
            comboBoxProducts.DisplayMember = nameof(Product.ProductName);
            comboBoxProducts.ValueMember = nameof(Product.ProductID);
            comboBoxProducts.Text = "";

        }

        public override void GetData()
        {
            if (storeViewModel.CurrentEntity == null) return;
            textBoxID.Text = storeViewModel.CurrentEntity.StoreTransactionID.ToString();
            comboBoxStoreID.SelectedValue = storeViewModel.CurrentEntity.StoreID;
            comboBoxPersonID.SelectedValue = storeViewModel.CurrentEntity.PersonID;
            dateTimeInsertDate.Value = storeViewModel.CurrentEntity.InsertDate;
            productTransactionBindingSource.DataSource = storeViewModel.CurrentEntity.ProductTransactions;
            base.GetData();

        }
        public override void SetData()
        {
            storeViewModel.CurrentEntity.StoreTransactionID = int.Parse(textBoxID.Text);
            storeViewModel.CurrentEntity.StoreID = (int)comboBoxStoreID.SelectedValue;
            storeViewModel.CurrentEntity.PersonID = (int)comboBoxPersonID.SelectedValue;
            storeViewModel.CurrentEntity.InsertDate = dateTimeInsertDate.Value;
            storeViewModel.CurrentEntity.InTransaction = Type == TransactionType.TransactionIn;
            foreach (var item in storeViewModel.CurrentEntity.ProductTransactions)
            {
                item.StoreID = storeViewModel.CurrentEntity.StoreID;
                item.InTransaction = storeViewModel.CurrentEntity.InTransaction;
            }

            base.SetData();
        }
        void Clean()
        {
            textBoxID.Text = string.Empty;
            comboBoxStoreID.Text = string.Empty;
            dateTimeInsertDate.Text = string.Empty;
        }
        public override void Find(int id)
        {
            storeViewModel.Find(id);
        }
        public override void New()
        {
            Clean();
            storeViewModel.New();
            productTransactionBindingSource.DataSource = storeViewModel.CurrentEntity.ProductTransactions;
            base.New();
        }
        public override bool CanSave()
        {
            return storeViewModel.CurrentEntity.StoreTransactionID <= 0;
        }
        public override void Save()
        {
            storeViewModel.Save();
            base.Save();
        }
        public override void Update()
        {
            storeViewModel.Update();
            base.Update();
        }
        public override void Delete()
        {
            storeViewModel.Delete();
            base.Delete();
        }

        private void comboBoxProducts_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (int.TryParse(comboBoxProducts.SelectedValue?.ToString(), out int productID))
            {
                var Units = (storeViewModel as StoreTransactionViewModel).GetUnits(productID);
                comboBoxUnits.DataSource = Units;
                comboBoxUnits.DisplayMember = nameof(UnitOfMeasurement.UnitName);
                comboBoxUnits.ValueMember = nameof(UnitOfMeasurement.UnitOfMeasurementID);
                comboBoxUnits.SelectedValue = Units.FirstOrDefault()?.UnitOfMeasurementID;
                comboBoxUnits.Text = string.Empty;

            }
            else
            {
                comboBoxUnits.DataSource = null;
                comboBoxUnits.Text = string.Empty;
            }

        }
        private void ComboBoxUnits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (int.TryParse(comboBoxProducts.SelectedValue?.ToString(), out int productID) && int.TryParse(comboBoxUnits.SelectedValue?.ToString(), out int unitID))
            {
                var Unit = (storeViewModel as StoreTransactionViewModel).GetUnits(productID).FirstOrDefault(x => x.UnitOfMeasurementID == unitID);
                textBoxPrice.Text = Unit.Price.ToString();
            }
        }
        private void DgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvItems.Columns[e.ColumnIndex].Name == "productIDDataGridViewTextBoxColumn" && int.TryParse(e.Value.ToString(), out int ProductID))
            {
                e.Value = ProductList.Find(x => x.ProductID == ProductID)?.ProductName;
                e.FormattingApplied = true;
            }
            if (dgvItems.Columns[e.ColumnIndex].Name == "unitOfMeasurementID" && int.TryParse(e.Value.ToString(), out int UnitID))
            {
                e.Value = UnitList.Find(x => x.UnitOfMeasurementID == UnitID)?.UnitName;
                e.FormattingApplied = true;
            }
        }
        int selectedRow = -1;
        private void DgvItems_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                selectedRow = dgvItems.SelectedRows[0].Index;
                SetProductTransaction(storeViewModel.CurrentEntity.ProductTransactions[selectedRow]);
            }
            catch (Exception)
            {

            }
        }

        void CleanSub()
        {
            comboBoxProducts.Text = string.Empty;
            comboBoxUnits.Text = string.Empty;
            textBoxQty.Text = string.Empty;
            textBoxPrice.Text = string.Empty;
            dateTimeProductionDate.Text = string.Empty;
            dateTimeExpiryDate.Text = string.Empty;
        }
        void SetProductTransaction(ProductTransaction productTransaction)
        {
            comboBoxProducts.SelectedValue = productTransaction.ProductID;
            comboBoxUnits.SelectedValue = productTransaction.unitOfMeasurementID;
            textBoxQty.Text = productTransaction.Quantity.ToString();
            textBoxPrice.Text = productTransaction.Price.ToString();
            dateTimeProductionDate.Text = productTransaction.ProductionDate.ToString();
            dateTimeExpiryDate.Text = productTransaction.ExpiryDate.ToString();
        }
        ProductTransaction GetProductTransaction()
        {
            StringBuilder errors = new StringBuilder();
            if (!int.TryParse(comboBoxProducts.SelectedValue?.ToString(), out int productID)) errors.AppendLine("تأكد من تحديد الصنف");
            if (!int.TryParse(comboBoxUnits.SelectedValue?.ToString(), out int unitID)) errors.AppendLine("تأكد من تحديد الوحدة");
            if (!decimal.TryParse(textBoxQty.Text.ToString(), out decimal Qty)) errors.AppendLine("تأكد من تحديد الكمية");
            if (!decimal.TryParse(textBoxPrice.Text.ToString(), out decimal Price)) errors.AppendLine("تأكد من تحديد السعر");
            if (!DateTime.TryParse(dateTimeProductionDate.Text.ToString(), out DateTime productionDate)) errors.AppendLine("تأكد من تحديد تاريخ الانتاج");
            if (!DateTime.TryParse(dateTimeExpiryDate.Text.ToString(), out DateTime expiryDate)) errors.AppendLine("تأكد من تحديد تاريخ الصلاحية");

            if (string.IsNullOrWhiteSpace(errors.ToString()))
            {
                ProductTransaction row = new ProductTransaction();
                row.ProductID = productID;
                row.unitOfMeasurementID = unitID;
                row.Quantity = Qty;
                row.Price = Price;
                row.ProductionDate = productionDate;
                row.ExpiryDate = expiryDate;

                return row;
            }
            else
            {
                MessageBox.Show(errors.ToString());
                return null;
            }
        }
        private void button1_Click(object sender, System.EventArgs e)
        {
            var row = GetProductTransaction();
            if (row != null)
                storeViewModel.CurrentEntity.ProductTransactions.Add(row);
            CleanSub();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var row = GetProductTransaction();
            if (row != null)
            {
                storeViewModel.CurrentEntity.ProductTransactions[selectedRow] = GetProductTransaction();
                selectedRow = -1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
                storeViewModel.CurrentEntity.ProductTransactions.RemoveAt(dgvItems.SelectedRows[0].Index);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CleanSub();
        }
    }
}
