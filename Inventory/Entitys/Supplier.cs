namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;

    public partial class Supplier : INotifyPropertyChanged
    {
        private int supplierID;
        public int SupplierID
        {
            get { return supplierID; }
            set
            {
                if (supplierID != value)
                {
                    supplierID = value;
                    OnPropertyChanged();
                }
            }
        }

        private string supplierName;
        [Required]
        [StringLength(255)]
        public string SupplierName
        {
            get { return supplierName; }
            set
            {
                if (supplierName != value)
                {
                    supplierName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string phone;
        [StringLength(50)]
        public string Phone
        {
            get { return phone; }
            set
            {
                if (phone != value)
                {
                    phone = value;
                    OnPropertyChanged();
                }
            }
        }

        private string fax;
        [StringLength(50)]
        public string Fax
        {
            get { return fax; }
            set
            {
                if (fax != value)
                {
                    fax = value;
                    OnPropertyChanged();
                }
            }
        }

        private string mobile;
        [StringLength(50)]
        public string Mobile
        {
            get { return mobile; }
            set
            {
                if (mobile != value)
                {
                    mobile = value;
                    OnPropertyChanged();
                }
            }
        }

        private string email;
        [StringLength(255)]
        public string Email
        {
            get { return email; }
            set
            {
                if (email != value)
                {
                    email = value;
                    OnPropertyChanged();
                }
            }
        }

        private string website;
        [StringLength(255)]
        public string Website
        {
            get { return website; }
            set
            {
                if (website != value)
                {
                    website = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
