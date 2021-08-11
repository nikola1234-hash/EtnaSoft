using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IUpdateBookingService
    {
        bool Update(Reservation reservation, Guest guest, string roomNumber, StayType stayType);
    }
}