using Inventory.Entitys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory.Views
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            ProjectFullStack db = new ProjectFullStack();
            db.Database.CreateIfNotExists();
        }
        void ShowForm(Form form) { form.ShowDialog(); }
        private void toolStripButton1_Click(object sender, EventArgs e) => ShowForm(new StoreForm());

        private void toolStripButton2_Click(object sender, EventArgs e) => ShowForm(new ProductForm());

        private void toolStripButton3_Click(object sender, EventArgs e) => ShowForm(new CustomerForm());

        private void toolStripButton4_Click(object sender, EventArgs e) => ShowForm(new SupplierForm());

        private void toolStripButton5_Click(object sender, EventArgs e) => ShowForm(new StoreTransactionForm(TransactionType.TransactionIn));

        private void toolStripButton6_Click(object sender, EventArgs e) => ShowForm(new StoreTransactionForm(TransactionType.TransactionOut));

        private void toolStripButton7_Click(object sender, EventArgs e) => ShowForm(new StoreTransferForm());

        private void تقريرالمخازنToolStripMenuItem_Click(object sender, EventArgs e) => ShowForm(new StoreReportForm());

        private void تقريرالاصنافToolStripMenuItem_Click(object sender, EventArgs e) => ShowForm(new ProductReportForm());

        private void تقريرالاصنافToolStripMenuItem1_Click(object sender, EventArgs e) => ShowForm(new ProductsReportForm());

        private void تقريرالاصنافالتيToolStripMenuItem_Click(object sender, EventArgs e) => ShowForm(new StoreDateReportForm());

        private void تقريرصلاحيةالاصنافToolStripMenuItem_Click(object sender, EventArgs e) => ShowForm(new ExpiryDateReportForm());

        private void حولToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("تطوير -محمد سلامة عوض  \n Fullstack.Net G1 Intake 6-2023/2024");
        }
    }
}
