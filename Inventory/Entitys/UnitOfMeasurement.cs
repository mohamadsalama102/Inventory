namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;

    public partial class UnitOfMeasurement : INotifyPropertyChanged
    {
        private int unitOfMeasurementID;
        public int UnitOfMeasurementID
        {
            get { return unitOfMeasurementID; }
            set
            {
                if (unitOfMeasurementID != value)
                {
                    unitOfMeasurementID = value;
                    OnPropertyChanged();
                }
            }
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

 
        private string unitName;
        [Required]
        [StringLength(255)]
        public string UnitName
        {
            get { return unitName; }
            set
            {
                if (unitName != value)
                {
                    unitName = value;
                    OnPropertyChanged();
                }
            }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
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
