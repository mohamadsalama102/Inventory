using Inventory.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModel
{
    internal class StoreTransferViewModel : BaseViewModel<StoreTransfer>
    {
        public override void New()
        {
            CurrentEntity = new StoreTransfer();
            CurrentEntity.ProductTransfers = new System.ComponentModel.BindingList<ProductTransfer>();
        }

        public List<Customer> GetCustomers() => db.Customers.ToList();
        public List<Supplier> GetSuppliers() => db.Suppliers.ToList();
        public List<Store> GetStores() => db.Stores.ToList();
        public List<Product> GetProducts() => db.Products.ToList();
        public List<UnitOfMeasurement> GetUnits(int productId) => productId==-1 ? db.UnitOfMeasurements.ToList():db.UnitOfMeasurements.Where(x => x.ProductID == productId).ToList();
    }
}
