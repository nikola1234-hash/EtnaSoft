﻿using System.Collections.Generic;
using System.Linq;
using Dapper;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Infrastucture;

namespace EtnaSoft.Dal.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private const string LoadAllUsers = "SELECT * FROM dbo.Users";
        private const string LoadById = "SELECT * FROM dbo.Users WHERE Id = @Id";

        private const string UpdateById = "sp_UpdateUser";

        private const string SetInactive = "Update dbo.Users Set IsActive = 0 WHERE Id = @Id";
        
        private const string SetActive = "Update dbo.Users Set IsActive = 1 WHERE Id = @Id";

        private const string CreateUser = "sp_CreateUser";


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
            var updateUser = new
            {
                Id= id,
                Name = user.Name,
                LastName = user.LastName,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                ModifiedBy = user.ModifiedBy
            };
            bool output = false;
            int i = _context.SaveData(UpdateById, updateUser);
            if (i == 1)
            {
                output = true;
            }

            return output;
        }

        public User Create(User user)
        {
            var parameters = new
            {
                Name = user.Name,
                LastName = user.LastName,
                Username = user.Username,
                PasswordHash = user.PasswordHash,
                CreatedBy = user.CreatedBy


            };
            var createdUser = _context.LoadData<User, dynamic>(CreateUser, parameters).FirstOrDefault();
            return createdUser;
        }
        public bool Delete(int id)
        {
            bool output = false;
            var user = GetById(id);
            var sqlParameter = SetInactive;
            if (!user.IsActive)
            {
                sqlParameter = SetActive;
            }
            
            int i = _context.SaveData(sqlParameter, new {Id = id});
            if (i == 1)
            {
                output = true;
            }

            return output;
        }
      
    }
}
