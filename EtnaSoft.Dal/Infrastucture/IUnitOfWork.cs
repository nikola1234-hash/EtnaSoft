using ErtnaSoft.Bo.Entities;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Dal.Infrastucture
{
    public interface IUnitOfWork
    {
        IRepository<Guest> Guests { get; }
        IRepository<User> Users { get; }
    }
}