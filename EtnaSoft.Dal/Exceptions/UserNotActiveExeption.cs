using System;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Dal.Exceptions
{
    public class UserNotActiveExeption :Exception
    {
        public User User { get; set; }
        public UserNotActiveExeption(User user)
        {
            User = user;
        }

        public UserNotActiveExeption(string message, User user) : base(message)
        {
            User = user;
        }

        public UserNotActiveExeption(string message, Exception innerException, User user) : base(message, innerException)
        {
            User = user;
        }
    }
}
