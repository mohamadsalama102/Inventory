using Inventory.ViewModel;
using System.Windows.Forms;

namespace Inventory.Views
{
    public partial class StoreForm : CRUDForm
    {
        internal IViewModel<Store> storeViewModel;
        public StoreForm()
        {
            InitializeComponent();
            storeViewModel = new StoreViewModel();

        }

        public override void GetData()
        {
            if (storeViewModel.CurrentEntity == null) return;
            textBoxStoreID.Text = storeViewModel.CurrentEntity.StoreID.ToString();
            textBoxStoreName.Text = storeViewModel.CurrentEntity.StoreName;
            textBoxStoreAddress.Text = storeViewModel.CurrentEntity.StoreAddress;
            textBoxResponsiblePerson.Text = storeViewModel.CurrentEntity.ResponsiblePerson;

            base.GetData();

        }
        public override void SetData()
        {
            storeViewModel.CurrentEntity.StoreID = int.Parse(textBoxStoreID.Text);
            storeViewModel.CurrentEntity.StoreName = textBoxStoreName.Text;
            storeViewModel.CurrentEntity.StoreAddress = textBoxStoreAddress.Text;
            storeViewModel.CurrentEntity.ResponsiblePerson = textBoxResponsiblePerson.Text;
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
            return storeViewModel.CurrentEntity.StoreID <= 0;
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
