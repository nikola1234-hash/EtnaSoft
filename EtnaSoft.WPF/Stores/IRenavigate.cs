﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using EtnaSoft.WPF.ViewModels;

namespace EtnaSoft.WPF.Stores
{
    public interface IRenavigate
    {
        EtnaBaseViewModel Navigate();
    }
}
