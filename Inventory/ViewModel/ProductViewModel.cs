using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Inventory.ViewModel
{
    internal class ProductViewModel :  BaseViewModel<Product> 
    {
        public override void New()
        {
            CurrentEntity = new Product();
        }
    }
}
