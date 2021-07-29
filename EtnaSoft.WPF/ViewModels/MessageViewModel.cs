﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;

namespace EtnaSoft.WPF.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {

        public bool HasMessage => !string.IsNullOrWhiteSpace(Message);

        private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                _message = value;
                RaisePropertiesChanged(nameof(Message));
                RaisePropertiesChanged(nameof(HasMessage));
            }
        }
    }
}
