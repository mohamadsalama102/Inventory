namespace Inventory
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public partial class Store : INotifyPropertyChanged
    {
        public Store()
        {
            StoreProducts = new HashSet<ProductTransaction>();
        }

        private int storeID;
        public int StoreID
        {
            get { return storeID; }
            set
            {
                if (storeID != value)
                {
                    storeID = value;
                    OnPropertyChanged();
                }
            }
        }

        private string storeName;
        [Required]
        [StringLength(255)]
        public string StoreName
        {
            get { return storeName; }
            set
            {
                if (storeName != value)
                {
                    storeName = value;
                    OnPropertyChanged();
                }
            }
        }

        private string storeAddress;
        [StringLength(255)]
        public string StoreAddress
        {
            get { return storeAddress; }
            set
            {
                if (storeAddress != value)
                {
                    storeAddress = value;
                    OnPropertyChanged();
                }
            }
        }

        private string responsiblePerson;
        [StringLength(255)]
        public string ResponsiblePerson
        {
            get { return responsiblePerson; }
            set
            {
                if (responsiblePerson != value)
                {
                    responsiblePerson = value;
                    OnPropertyChanged();
                }
            }
        }

        public virtual ICollection<ProductTransaction> StoreProducts { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
