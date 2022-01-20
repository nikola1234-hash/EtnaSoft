using System.Collections.Generic;
using ErtnaSoft.Bo.Entities;

namespace EtnaSoft.Bll.Services
{
    public interface IUserManagerService
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByUsername(string username);
        bool SetActiveOrInactiveUser(int id);
    }
}