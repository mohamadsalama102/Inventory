using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.ViewModel
{
    public interface IViewModel<T> where T : class
    {
        T CurrentEntity { get; set; }
        void New();
        T Find(int id);
        T Save();
        T Update();
        bool Delete();
    }
}
