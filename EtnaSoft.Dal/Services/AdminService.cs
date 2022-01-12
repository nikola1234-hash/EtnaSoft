using System.Linq;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;
using Microsoft.AspNet.Identity;

namespace EtnaSoft.Dal.Services
{
    public class AdminService
    {
        private readonly IUnitOfWork _unit;
        private readonly IPasswordHasher _hasher;
        public AdminService(IUnitOfWork unit, IPasswordHasher hasher)
        {
            _unit = unit;
            _hasher = hasher;
        }
        /// <summary>
        /// Checking if Admin User Exists
        /// </summary>
        /// <returns>Boolean, true if exists.</returns>
        public bool CheckIfAccountExists()
        {
            bool output = false;
            string username = "Admin";
            var user = _unit.Users.GetAll().FirstOrDefault(s => s.Username == username);
            output = user != null;
            return output;
        }
        /// <summary>
        /// Creates First Admin user, which later it will be changed.
        /// </summary>
        public void FirstUserCreation()
        {
            string username = "Admin";
            var user = _unit.Users.GetAll().FirstOrDefault(s => s.Username == username);
            if (user is null)
            {
                string hashedPassword = _hasher.HashPassword("admin");
                User newUser = new User()
                {
                    Username = "Admin",
                    PasswordHash = hashedPassword,
                    Name = "Admin",
                    LastName = "Admin",
                    CreatedBy = "System"

                };
                var admin = _unit.Users.Create(newUser);
            }
        }
    }
}
