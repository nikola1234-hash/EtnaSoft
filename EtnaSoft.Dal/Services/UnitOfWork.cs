using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Repositories;


namespace EtnaSoft.Dal.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepository<ErtnaSoft.Bo.Entities.User> users, IRepository<Guest> guests, IRepository<Room> rooms,
            IRepository<Reservation> reservations, IRepository<RoomReservation> roomReservations,
            IRepository<StayType> stayTypes, IRepository<CustomLabel> labels, IRepository<DataGridGuest> guestDataGrid, IRepository<Promotion> promotions)
        {
            Guests = guests;
            Users = users;
            Rooms = rooms;
            Reservations = reservations;
            RoomReservations = roomReservations;
            StayTypes = stayTypes;
            Labels = labels;
            DataGridGuests = guestDataGrid;
            Promotions = promotions;
        }

        public IRepository<Guest> Guests { get;  }
        public IRepository<ErtnaSoft.Bo.Entities.User> Users { get; }
        public IRepository<Room> Rooms { get; }
        public IRepository<Reservation> Reservations { get; }
        public IRepository<RoomReservation> RoomReservations { get; }
        public IRepository<StayType> StayTypes { get; }
        public IRepository<CustomLabel> Labels { get; }
        public IRepository<DataGridGuest> DataGridGuests { get; }
        public IRepository<Promotion> Promotions { get; }

    }
}
