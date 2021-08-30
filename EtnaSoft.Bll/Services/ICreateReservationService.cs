using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface ICreateReservationService
    {
        void CreateReservationInTransaction(RoomReservation roomReservation, Reservation reservation);
        
    }
}
