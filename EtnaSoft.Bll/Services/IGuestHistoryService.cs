using System.Collections.Generic;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IGuestHistoryService
    {
        IEnumerable<GuestBookingHistory> GetGuestBookingHistory(int id);
    }
}