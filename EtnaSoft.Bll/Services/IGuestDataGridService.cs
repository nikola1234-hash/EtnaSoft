using System.Collections.Generic;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IGuestDataGridService
    {
        IEnumerable<DataGridGuest> GetAllGuests();
        bool UpdateGuestData(int id, DataGridGuest guest);
    }
}