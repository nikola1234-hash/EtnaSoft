using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Repositories;

namespace EtnaSoft.Dal.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepository<User> users, IRepository<Guest> guests, IRepository<Room> rooms,
            IRepository<Reservation> reservations, IRepository<RoomReservation> roomReservations, IRepository<StayType> stayTypes)
        {
            Guests = guests;
            Users = users;
            Rooms = rooms;
            Reservations = reservations;
            RoomReservations = roomReservations;
            StayTypes = stayTypes;
        }

        public IRepository<Guest> Guests { get; private set; }
        public IRepository<User> Users { get; private set; }
        public IRepository<Room> Rooms { get; private set; }
        public IRepository<Reservation> Reservations { get; private set; }
        public IRepository<RoomReservation> RoomReservations { get; private set; }
        public IRepository<StayType> StayTypes { get; }
    }
}
