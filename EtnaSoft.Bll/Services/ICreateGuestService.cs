using System;
using System.Collections.Generic;
using System.Text;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface ICreateGuestService
    {
        Guest CreateGuest(Guest guest);
        [Obsolete]
        void DeactivateGuest(int id);
    }
}
