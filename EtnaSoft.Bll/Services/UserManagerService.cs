using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Bll.Services
{
    public sealed class UserManagerService : IUserManagerService
    {
        private readonly IUnitOfWork _unit;

        public UserManagerService(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var user = _unit.Users.GetAll();
            return user;
        }

        public User GetUserByUsername(string username)
        {
            var user = _unit.Users.GetAll().FirstOrDefault(s => s.Username == username);
            return user;
        }

        public bool SetActiveOrInactiveUser(int id)
        {
            return _unit.Users.Delete(id);
            
        }
    }
}
