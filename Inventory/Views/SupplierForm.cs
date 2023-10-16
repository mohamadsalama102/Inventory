using Inventory.ViewModel;
using System.Windows.Forms;

namespace Inventory.Views
{
    public partial class SupplierForm : CRUDForm
    {
        internal IViewModel<Supplier> storeViewModel;
        public SupplierForm()
        {
            InitializeComponent();
            storeViewModel = new SupplierViewModel();

        }

        public override void GetData()
        {
            textBoxSupplierID.Text = storeViewModel.CurrentEntity.SupplierID.ToString();
            textBoxStoreName.Text = storeViewModel.CurrentEntity.SupplierName;
            textBoxEmail.Text = storeViewModel.CurrentEntity.Email;
            textBoxFax.Text = storeViewModel.CurrentEntity.Fax;
            textBoxMobile.Text = storeViewModel.CurrentEntity.Mobile;
            textBoxPhone.Text = storeViewModel.CurrentEntity.Phone;
            textBoxWebsite.Text = storeViewModel.CurrentEntity.Website;

            base.GetData();

        }
        public override void SetData()
        {
            storeViewModel.CurrentEntity.SupplierID = int.Parse(textBoxSupplierID.Text);
            storeViewModel.CurrentEntity.SupplierName = textBoxStoreName.Text;
            storeViewModel.CurrentEntity.Email = textBoxEmail.Text;
            storeViewModel.CurrentEntity.Fax = textBoxFax.Text;
            storeViewModel.CurrentEntity.Mobile = textBoxMobile.Text;
            storeViewModel.CurrentEntity.Phone = textBoxPhone.Text;
            storeViewModel.CurrentEntity.Website = textBoxWebsite.Text;
            base.SetData();
        }
        public override void Find(int id)
        {
            storeViewModel.Find(id);
        }
        public override void New()
        {
            storeViewModel.New();
            base.New();
        }
        public override bool CanSave()
        {
            return storeViewModel.CurrentEntity?.SupplierID <= 0;
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
