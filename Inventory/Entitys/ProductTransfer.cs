using Inventory.Entitys;
using System;

namespace Inventory
{
    public partial class ProductTransfer
    {
        public int ID { get; set; }
        public int StoreTransferID { get; set; }
        public StoreTransfer StoreTransfer { get; set; }
        public string State =>  InTransaction ?
            $"محول من ({StoreTransfer.StoreFrom.StoreName}) الي ({StoreTransfer.StoreTo.StoreName})" :
            $"تحويل من ({StoreTransfer.StoreTo.StoreName}) الي ({StoreTransfer.StoreFrom.StoreName})";
        public int StoreID { get; set; }

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
