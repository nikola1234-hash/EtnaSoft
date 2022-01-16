using System;

namespace EtnaSoft.Dal.Services.UserServices
{
    public interface IUserService
    {
        bool DoesUserExists(Func<ErtnaSoft.Bo.Entities.User,bool> predicate);
    }
}