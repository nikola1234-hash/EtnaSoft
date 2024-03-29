﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EtnaSoft.WPF.Services.Authentication
{
    public interface IAuthenticator
    {
        bool IsLoggedIn { get; }
        bool Login(string username, string password);
        void Logout();
    }
}
