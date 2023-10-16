using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Inventory
{
    public partial class ProductTransaction
    {
        public int ID { get; set; }
        public int StoreTransactionID { get; set; }

        public int StoreID { get; set; }
        public Store Store  { get; set; }

        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ProductionDate { get; set; } = DateTime.Now;
        public DateTime ExpiryDate { get; set; } = DateTime.Now;
       
        public int unitOfMeasurementID { get; set; }
        public UnitOfMeasurement UnitOfMeasurement { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }

        public bool InTransaction { get; set; }

    }  
}
