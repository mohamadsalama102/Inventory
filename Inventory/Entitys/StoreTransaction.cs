using System;
using System.ComponentModel;

namespace Inventory.Entitys
{
    public partial class StoreTransaction
    {
        public StoreTransaction()
        {
            ProductTransactions = new BindingList<ProductTransaction>();
        }
        public int StoreTransactionID { get; set; }
        public int StoreID { get; set; }
        public virtual Store Store { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public bool InTransaction { get; set; }
        public int PersonID { get; set; }
        public virtual BindingList<ProductTransaction> ProductTransactions { get; set; }
    }
}
