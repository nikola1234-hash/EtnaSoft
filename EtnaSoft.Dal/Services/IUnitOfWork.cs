using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Repositories;

namespace EtnaSoft.Dal.Services
{
    public interface IUnitOfWork
    {
        IRepository<Guest> Guests { get; }
        IRepository<User> Users { get; }
    }
}