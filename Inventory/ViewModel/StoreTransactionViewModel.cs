using Inventory.Entitys;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModel
{
    internal class StoreTransactionViewModel : BaseViewModel<StoreTransaction>
    {
        public override void New()
        {
            CurrentEntity = new StoreTransaction();
            CurrentEntity.ProductTransactions = new System.ComponentModel.BindingList<ProductTransaction>();
        }
        public List<Customer> GetCustomers() => db.Customers.ToList();
        public List<Supplier> GetSuppliers() => db.Suppliers.ToList();
        public List<Store> GetStores() => db.Stores.ToList();
        public List<Product> GetProducts() => db.Products.ToList();
        public List<UnitOfMeasurement> GetUnits(int productId) => productId==-1 ? db.UnitOfMeasurements.ToList():db.UnitOfMeasurements.Where(x => x.ProductID == productId).ToList();
    }
}
