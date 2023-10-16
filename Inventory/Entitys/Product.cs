namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;

    public partial class Product : INotifyPropertyChanged
    {
        public Product()
        {
            StoreProducts = new HashSet<ProductTransaction>();
            UnitOfMeasurements = new BindingList<UnitOfMeasurement>();
        }
        private int productID;
        public int ProductID
        {
            get { return productID; }
            set
            {
                if (productID != value)
                {
                    productID = value;
                    OnPropertyChanged();
                }
            }
        }

        private string productCode;
        [Required]
        [StringLength(50)]
        public string ProductCode
        {
            get { return productCode; }
            set
            {
                if (productCode != value)
                {
                    productCode = value;
                    OnPropertyChanged();
                }
            }
        }

        private string productName;
        [Required]
        [StringLength(255)]
        public string ProductName
        {
            get { return productName; }
            set
            {
                if (productName != value)
                {
                    productName = value;
                    OnPropertyChanged();
                }
            }
        }

      

        public virtual ICollection<ProductTransaction> StoreProducts { get; set; }
        public virtual BindingList<UnitOfMeasurement> UnitOfMeasurements { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
