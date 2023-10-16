using Inventory.Entitys;
using Inventory.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Inventory.Views
{

    public partial class StoreTransferForm : CRUDForm
    {
        internal IViewModel<StoreTransfer> storeViewModel;


        public StoreTransferForm()
        {
            InitializeComponent();
            storeViewModel = new StoreTransferViewModel();


            InitializeEvent();
            InitializeData();

        }
        void InitializeEvent()
        {
            this.comboBoxProducts.SelectedIndexChanged += comboBoxProducts_SelectedIndexChanged;
            this.comboBoxUnits.SelectedIndexChanged += ComboBoxUnits_SelectedIndexChanged;
            dgvItems.CellFormatting += DgvItems_CellFormatting;
            dgvItems.DoubleClick += DgvItems_DoubleClick;
        }


        List<Product> ProductList;
        List<UnitOfMeasurement> UnitList;
        void InitializeData()
        {
            lblPersonType.Text = "مورد";
            comboBoxPersonID.DataSource = (storeViewModel as StoreTransferViewModel).GetSuppliers();
            comboBoxPersonID.DisplayMember = nameof(Supplier.SupplierName);
            comboBoxPersonID.ValueMember = nameof(Supplier.SupplierID);

            comboBoxStoreFromID.DataSource = (storeViewModel as StoreTransferViewModel).GetStores();
            comboBoxStoreFromID.DisplayMember = nameof(Store.StoreName);
            comboBoxStoreFromID.ValueMember = nameof(Store.StoreID);

            comboBoxStoreToID.DataSource = (storeViewModel as StoreTransferViewModel).GetStores();
            comboBoxStoreToID.DisplayMember = nameof(Store.StoreName);
            comboBoxStoreToID.ValueMember = nameof(Store.StoreID);

            UnitList = (storeViewModel as StoreTransferViewModel).GetUnits(-1);
            ProductList = (storeViewModel as StoreTransferViewModel).GetProducts();
            comboBoxProducts.DataSource = ProductList;
            comboBoxProducts.DisplayMember = nameof(Product.ProductName);
            comboBoxProducts.ValueMember = nameof(Product.ProductID);
            comboBoxProducts.Text = "";

        }

        public override void GetData()
        {
            if (storeViewModel.CurrentEntity == null) return;
            textBoxID.Text = storeViewModel.CurrentEntity.ID.ToString();
            comboBoxStoreFromID.SelectedValue = storeViewModel.CurrentEntity.StoreFromID;
            comboBoxStoreToID.SelectedValue = storeViewModel.CurrentEntity.StoreToID;
            comboBoxPersonID.SelectedValue = storeViewModel.CurrentEntity.PersonID;
            dateTimeInsertDate.Value = storeViewModel.CurrentEntity.InsertDate;
            productTransferBindingSource.DataSource = storeViewModel.CurrentEntity.ProductTransfers;
            base.GetData();

        }
        public override void SetData()
        {
            storeViewModel.CurrentEntity.ID = int.Parse(textBoxID.Text);
            storeViewModel.CurrentEntity.StoreFromID = (int)comboBoxStoreFromID.SelectedValue;
            storeViewModel.CurrentEntity.StoreToID = (int)comboBoxStoreToID.SelectedValue;
            storeViewModel.CurrentEntity.PersonID = (int)comboBoxPersonID.SelectedValue;
            storeViewModel.CurrentEntity.InsertDate = dateTimeInsertDate.Value;

            //Remove the previous product Transfer
            var RemoveItems = storeViewModel.CurrentEntity.ProductTransfers.Where(x => x.InTransaction).ToList();
            RemoveItems.ForEach(x => storeViewModel.CurrentEntity.ProductTransfers.Remove(x));

            foreach (var item in storeViewModel.CurrentEntity.ProductTransfers)
            {
                item.StoreID = storeViewModel.CurrentEntity.StoreFromID;
                item.InTransaction = false;
            }

            int count = storeViewModel.CurrentEntity.ProductTransfers.Count;
            for (int i = 0; i < count; i++)
            {
                ProductTransfer productTransfer = new ProductTransfer();
                productTransfer.StoreID = storeViewModel.CurrentEntity.StoreToID;
                productTransfer.InTransaction = true;
                productTransfer.ProductID = storeViewModel.CurrentEntity.ProductTransfers[i].ProductID;
                productTransfer.unitOfMeasurementID = storeViewModel.CurrentEntity.ProductTransfers[i].unitOfMeasurementID;
                productTransfer.Quantity = storeViewModel.CurrentEntity.ProductTransfers[i].Quantity;
                productTransfer.ProductionDate = storeViewModel.CurrentEntity.ProductTransfers[i].ProductionDate;
                productTransfer.ExpiryDate = storeViewModel.CurrentEntity.ProductTransfers[i].ExpiryDate;
                productTransfer.Price = storeViewModel.CurrentEntity.ProductTransfers[i].Price;
                storeViewModel.CurrentEntity.ProductTransfers.Add(productTransfer);
            }
            base.SetData();
        }
        void Clean()
        {
            textBoxID.Text = string.Empty;
            comboBoxStoreFromID.Text = string.Empty;
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
            productTransferBindingSource.DataSource = storeViewModel.CurrentEntity.ProductTransfers;
            base.New();
        }
        public override bool CanSave()
        {
            return storeViewModel.CurrentEntity.ID <= 0;
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
                var Units = (storeViewModel as StoreTransferViewModel).GetUnits(productID);
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
                var Unit = (storeViewModel as StoreTransferViewModel).GetUnits(productID).FirstOrDefault(x => x.UnitOfMeasurementID == unitID);
                textBoxPrice.Text = Unit.Price.ToString();
            }
        }
        private void DgvItems_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvItems.Columns[e.ColumnIndex].Name == "productIDDataGridViewTextBoxColumn" && int.TryParse(e.Value?.ToString(), out int ProductID))
            {
                e.Value = ProductList.Find(x => x.ProductID == ProductID)?.ProductName;
                e.FormattingApplied = true;
            }
            if (dgvItems.Columns[e.ColumnIndex].Name == "unitOfMeasurementID" && int.TryParse(e.Value?.ToString(), out int UnitID))
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
                SetProductTransfer(storeViewModel.CurrentEntity.ProductTransfers[selectedRow]);
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
        void SetProductTransfer(ProductTransfer ProductTransfer)
        {
            comboBoxProducts.SelectedValue = ProductTransfer.ProductID;
            comboBoxUnits.SelectedValue = ProductTransfer.unitOfMeasurementID;
            textBoxQty.Text = ProductTransfer.Quantity.ToString();
            textBoxPrice.Text = ProductTransfer.Price.ToString();
            dateTimeProductionDate.Text = ProductTransfer.ProductionDate.ToString();
            dateTimeExpiryDate.Text = ProductTransfer.ExpiryDate.ToString();
        }
        ProductTransfer GetProductTransfer()
        {
            StringBuilder errors = new StringBuilder();
            if (!int.TryParse(comboBoxStoreFromID.SelectedValue?.ToString(), out int FromID)) errors.AppendLine("تأكد من تحديد المخزن أولا");
            if (!int.TryParse(comboBoxProducts.SelectedValue?.ToString(), out int productID)) errors.AppendLine("تأكد من تحديد الصنف");
            if (!int.TryParse(comboBoxUnits.SelectedValue?.ToString(), out int unitID)) errors.AppendLine("تأكد من تحديد الوحدة");
            if (!decimal.TryParse(textBoxQty.Text.ToString(), out decimal Qty)) errors.AppendLine("تأكد من تحديد الكمية");
            if (!decimal.TryParse(textBoxPrice.Text.ToString(), out decimal Price)) errors.AppendLine("تأكد من تحديد السعر");
            if (!DateTime.TryParse(dateTimeProductionDate.Text.ToString(), out DateTime productionDate)) errors.AppendLine("تأكد من تحديد تاريخ الانتاج");
            if (!DateTime.TryParse(dateTimeExpiryDate.Text.ToString(), out DateTime expiryDate)) errors.AppendLine("تأكد من تحديد تاريخ الصلاحية");

            if (string.IsNullOrWhiteSpace(errors.ToString()))
            {
                ProductTransfer row = new ProductTransfer();
                row.StoreID = FromID;
                row.InTransaction = false;
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
            var row = GetProductTransfer();
            if (row != null)
                storeViewModel.CurrentEntity.ProductTransfers.Add(row);
            CleanSub();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var row = GetProductTransfer();
            if (row != null)
            {
                storeViewModel.CurrentEntity.ProductTransfers[selectedRow] = GetProductTransfer();
                selectedRow = -1;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dgvItems.SelectedRows.Count > 0)
                storeViewModel.CurrentEntity.ProductTransfers.RemoveAt(dgvItems.SelectedRows[0].Index);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            CleanSub();
        }
    }
}
