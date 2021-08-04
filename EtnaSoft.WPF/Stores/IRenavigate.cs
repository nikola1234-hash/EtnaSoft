using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace EtnaSoft.WPF.Stores
{
    public interface IRenavigate
    {
        ViewModelBase Navigate();
    }
}
