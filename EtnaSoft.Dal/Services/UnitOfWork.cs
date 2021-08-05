using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Repositories;

namespace EtnaSoft.Dal.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IRepository<User> users, IRepository<Guest> guests, IRepository<Room> rooms)
        {
            Guests = guests;
            Users = users;
            Rooms = rooms;
        }

        public IRepository<Guest> Guests { get; private set; }
        public IRepository<User> Users { get; private set; }
        public IRepository<Room> Rooms { get; private set; }



    }
}
