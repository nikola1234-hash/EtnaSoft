using System.Collections.Generic;
using System.Linq;
using Dapper;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class UserRepository
    {
        private const string LoadAllUsers = "SELECT * FROM dbo.User";
        private const string LoadById = "SELECT * FROM dbo.User WHERE Id = @Id";

        private const string UpdateById =
            "UPDATE dbo.User SET Username = @Username, PasswordHash = @PasswordHash, Name = @Name, LastName = @LastName, IsActive = @IsActive WHERE Id = @Id";

        private const string SetInactive = "Update dbo.User Set IsActive = 0 WHERE Id = @Id";

        private const string CreateUser = "Insert into dbo.User (Name, LastName, Username, PasswordHash)" +
                                          "Values (@Name, @LastName, @Username, @PasswordHash";


        private readonly IGenericDbContext _context;

        public UserRepository(IGenericDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            var output = _context.LoadData<User, dynamic>(LoadAllUsers, new { });
            return output;
        }

        public User GetById(int id)
        {
            var output = _context.LoadData<User, dynamic>(LoadById, new {Id = id}).FirstOrDefault();
            return output;
        }

        public bool Update(int id, User user)
        {
            user.Id = id;
            DynamicParameters o = new DynamicParameters(user);
            bool output = false;
            int i = _context.SaveData(UpdateById, o);
            if (i == 1)
            {
                output = true;
            }

            return output;
        }

        public User Create(User user)
        {
            DynamicParameters userToCreate = new DynamicParameters(user);
            var createdUser = _context.LoadData<User, dynamic>(CreateUser, userToCreate).FirstOrDefault();
            return createdUser;
        }
        public bool Delete(int id)
        {
            bool output = false;
            int i = _context.SaveData(SetInactive, new {Id = id});
            if (i == 1)
            {
                output = true;
            }

            return output;
        }
      
    }
}
