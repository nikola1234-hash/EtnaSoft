using System;
using System.Linq;
using System.Linq.Expressions;
using EtnaSoft.Dal.Infrastucture;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Dal.Services.UserServices
{
    //TODO: Register Dependency
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unit;

        public UserService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public bool DoesUserExists(Func<ErtnaSoft.Bo.Entities.User,bool> predicate)
        {
            bool success = false;
            var result = _unit.Users.GetAll().FirstOrDefault(predicate);
            if (result != null)
            {

                success = true;
               
            }
            return success;
        }
    }
}
