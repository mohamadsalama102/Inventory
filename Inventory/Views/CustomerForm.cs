using Inventory.ViewModel;
using System.Windows.Forms;

namespace Inventory.Views
{
    public partial class CustomerForm : CRUDForm
    {
        internal IViewModel<Customer> storeViewModel;
        public CustomerForm()
        {
            InitializeComponent();
            storeViewModel = new CustomerViewModel();

        }

        public override void GetData()
        {
            textBoxCustomerID.Text = storeViewModel.CurrentEntity.CustomerID.ToString();
            textBoxStoreName.Text = storeViewModel.CurrentEntity.CustomerName;
            textBoxEmail.Text = storeViewModel.CurrentEntity.Email;
            textBoxFax.Text = storeViewModel.CurrentEntity.Fax;
            textBoxMobile.Text = storeViewModel.CurrentEntity.Mobile;
            textBoxPhone.Text = storeViewModel.CurrentEntity.Phone;
            textBoxWebsite.Text = storeViewModel.CurrentEntity.Website;

            base.GetData();

        }
        public override void SetData()
        {
            storeViewModel.CurrentEntity.CustomerID = int.Parse(textBoxCustomerID.Text);
            storeViewModel.CurrentEntity.CustomerName = textBoxStoreName.Text;
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
            return storeViewModel.CurrentEntity.CustomerID <= 0;
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
