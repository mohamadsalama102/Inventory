using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModel
{
    internal class CustomerViewModel :  BaseViewModel<Customer>
    {
        public override void New()
        {
            CurrentEntity = new Customer();
        }
    }
}
