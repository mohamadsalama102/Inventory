using Inventory.ViewModel;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Inventory.Views
{
    public partial class ProductForm : CRUDForm
    {
        internal IViewModel<Product> storeViewModel;
        public ProductForm()
        {
            InitializeComponent();
            storeViewModel = new ProductViewModel();
        }

        public override void GetData()
        {
            textBoxProductID.Text = storeViewModel.CurrentEntity.ProductID.ToString();
            textBoxProductCode.Text = storeViewModel.CurrentEntity.ProductCode;
            textBoxProductName.Text = storeViewModel.CurrentEntity.ProductName;
            unitOfMeasurementBindingSource.DataSource = storeViewModel.CurrentEntity.UnitOfMeasurements;
            base.GetData();

        }
        public override void SetData()
        {
            storeViewModel.CurrentEntity.ProductID = int.Parse(textBoxProductID.Text);
            storeViewModel.CurrentEntity.ProductCode = textBoxProductCode.Text;
            storeViewModel.CurrentEntity.ProductName = textBoxProductName.Text;
            base.SetData();
        }
        public override void Find(int id)
        {
            storeViewModel.Find(id);
        }
        public override void New()
        {
            storeViewModel.New();
            unitOfMeasurementBindingSource.DataSource = storeViewModel.CurrentEntity.UnitOfMeasurements;
            base.New();
        }
        public override bool CanSave()
        {
            return storeViewModel.CurrentEntity?.ProductID <= 0;
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


    }
}
