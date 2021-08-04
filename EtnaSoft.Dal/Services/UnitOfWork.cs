using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Repositories;

namespace EtnaSoft.Dal.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(IGenericDbContext dbContext)
        {
            Guests = new GuestRepository(dbContext);
            Users = new UserRepository(dbContext);
        }

        public IRepository<Guest> Guests { get; private set; }
        public IRepository<User> Users { get; private set; }




    }
}
