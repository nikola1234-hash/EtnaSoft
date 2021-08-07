using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;


namespace EtnaSoft.Dal.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepository<User> users, IRepository<Guest> guests, IRepository<Room> rooms,
            IRepository<Reservation> reservations, IRepository<RoomReservation> roomReservations,
            IRepository<StayType> stayTypes)
        {
            Guests = guests;
            Users = users;
            Rooms = rooms;
            Reservations = reservations;
            RoomReservations = roomReservations;
            StayTypes = stayTypes;
        }

        public IRepository<Guest> Guests { get;  }
        public IRepository<User> Users { get; }
        public IRepository<Room> Rooms { get; }
        public IRepository<Reservation> Reservations { get; }
        public IRepository<RoomReservation> RoomReservations { get; }
        public IRepository<StayType> StayTypes { get; }
    }
}
