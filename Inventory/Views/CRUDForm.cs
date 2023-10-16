using System;
using System.Drawing;
using System.Windows.Forms;

namespace Inventory
{
    public partial class CRUDForm : Form
    {
        public CRUDForm()
        {
            InitializeComponent();
        }
        public virtual bool CanSave() { return true; }
        public virtual void GetData() { }
        public virtual void SetData() { }
        public virtual void Find(int id) { }
        public virtual void New() { }
        public virtual void Save()
        {

            MessageBox.Show("تمت الاضافة بنجاح");
        }
        public virtual void Update()
        {
            MessageBox.Show("تمت التعديلات بنجاح");
        }
        public virtual void Delete()
        {
            MessageBox.Show("تم الحذف بنجاح");
        }
        private void toolStripButton1_Click(object sender, EventArgs e) { New(); GetData(); }
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (CanSave())
            {
                SetData();
                Save();
            }
            else MessageBox.Show("لا يمكن الحفظ");
        }
        private void toolStripButton3_Click(object sender, EventArgs e) => Update();
        private void toolStripButton4_Click(object sender, EventArgs e) { Delete(); toolStripButton4.PerformClick(); }
        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            InputBox.SetLanguage(InputBox.Language.Arabic);
            DialogResult res = InputBox.ShowDialog("ادخل رقم العنصر",
            "بحث",
                InputBox.Icon.Question,
                InputBox.Buttons.OkCancel,
                InputBox.Type.TextBox,
               null,
                true,
                new Font("Calibri", 10F, FontStyle.Bold)); //Set font (default by system)

            if (res == DialogResult.OK || res == DialogResult.Yes)
                if (int.TryParse(InputBox.ResultValue, out int id)) { Find(id); GetData(); } //Get returned value
        }
    }
}
