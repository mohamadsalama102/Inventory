using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModel
{
    internal class SupplierViewModel :  BaseViewModel<Supplier>
    {
        public override void New()
        {
            CurrentEntity = new Supplier();
        }
    }
}
