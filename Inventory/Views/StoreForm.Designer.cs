namespace Inventory.Views
{
    partial class StoreForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBoxStoreName = new System.Windows.Forms.TextBox();
            this.textBoxStoreID = new System.Windows.Forms.TextBox();
            this.textBoxStoreAddress = new System.Windows.Forms.TextBox();
            this.textBoxResponsiblePerson = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBoxStoreName
            // 
            this.textBoxStoreName.Location = new System.Drawing.Point(54, 61);
            this.textBoxStoreName.Name = "textBoxStoreName";
            this.textBoxStoreName.Size = new System.Drawing.Size(232, 20);
            this.textBoxStoreName.TabIndex = 0;
            // 
            // textBoxStoreID
            // 
            this.textBoxStoreID.Location = new System.Drawing.Point(54, 35);
            this.textBoxStoreID.Name = "textBoxStoreID";
            this.textBoxStoreID.ReadOnly = true;
            this.textBoxStoreID.Size = new System.Drawing.Size(232, 20);
            this.textBoxStoreID.TabIndex = 3;
            // 
            // textBoxStoreAddress
            // 
            this.textBoxStoreAddress.Location = new System.Drawing.Point(54, 113);
            this.textBoxStoreAddress.Multiline = true;
            this.textBoxStoreAddress.Name = "textBoxStoreAddress";
            this.textBoxStoreAddress.Size = new System.Drawing.Size(232, 58);
            this.textBoxStoreAddress.TabIndex = 2;
            // 
            // textBoxResponsiblePerson
            // 
            this.textBoxResponsiblePerson.Location = new System.Drawing.Point(54, 87);
            this.textBoxResponsiblePerson.Name = "textBoxResponsiblePerson";
            this.textBoxResponsiblePerson.Size = new System.Drawing.Size(232, 20);
            this.textBoxResponsiblePerson.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 41);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "رقم";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "اسم";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(40, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "المسؤل";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 116);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "العنوان";
            // 
            // StoreForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 194);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxStoreAddress);
            this.Controls.Add(this.textBoxResponsiblePerson);
            this.Controls.Add(this.textBoxStoreName);
            this.Controls.Add(this.textBoxStoreID);
            this.Name = "StoreForm";
            this.Text = "المخزن";
            this.Controls.SetChildIndex(this.textBoxStoreID, 0);
            this.Controls.SetChildIndex(this.textBoxStoreName, 0);
            this.Controls.SetChildIndex(this.textBoxResponsiblePerson, 0);
            this.Controls.SetChildIndex(this.textBoxStoreAddress, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.label2, 0);
            this.Controls.SetChildIndex(this.label3, 0);
            this.Controls.SetChildIndex(this.label4, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxStoreName;
        private System.Windows.Forms.TextBox textBoxStoreID;
        private System.Windows.Forms.TextBox textBoxStoreAddress;
        private System.Windows.Forms.TextBox textBoxResponsiblePerson;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}