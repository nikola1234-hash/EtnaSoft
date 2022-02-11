using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface ICreateReservationService
    {
        bool CreateReservationInTransaction(RoomReservation roomReservation, Reservation reservation, Invoice invoice);
        
    }
}
