using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Inventory.Entitys
{
    public class StoreTransfer
    {
        public StoreTransfer()
        {
            ProductTransfers = new BindingList<ProductTransfer>();
        }
        public int ID { get; set; }
        public int PersonID { get; set; }
        public int StoreFromID { get; set; }
        [ForeignKey("StoreFromID")]
        public virtual Store StoreFrom { get; set; }
        public int StoreToID { get; set; }
        [ForeignKey("StoreToID")]
        public virtual Store StoreTo { get; set; }
        public DateTime InsertDate { get; set; } = DateTime.Now;
        public virtual BindingList<ProductTransfer>  ProductTransfers { get; set; }

    }
}
