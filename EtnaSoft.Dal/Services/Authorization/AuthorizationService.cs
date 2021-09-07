
using System;
using System.Linq;
using System.Text;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Exceptions;
using EtnaSoft.Dal.Infrastucture;
using Microsoft.AspNet.Identity;

namespace EtnaSoft.Dal.Services.Authorization
{
    public class AuthorizationService : IAuthorization
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _hasher;
        public AuthorizationService(IUnitOfWork unitOfWork, IPasswordHasher hasher)
        {
            _unitOfWork = unitOfWork;
            _hasher = hasher;
        }

        public User LoginUser(string username, string password)
        {
            if (String.IsNullOrWhiteSpace(username) || String.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Must have username and password!");
            }
            User user = _unitOfWork.Users.GetAll().FirstOrDefault(s => s.Username == username);
            if (user is null)
            {
                throw new Exception("Username does not exist");
            }

            if (user.IsActive == false)
            {
                throw new UserNotActiveExeption("This user is not active", user);
            }
            PasswordVerificationResult result = _hasher.VerifyHashedPassword(user.PasswordHash, password);
            
            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid Passowrd");
            }
            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }
            //This should fail
            //Should not have come this far..
            throw new Exception("Something is wrong!");
        }

        public RegistrationStatus RegisterUser(string name, string lastName, string username, string password,
            string repeatPassword)
        {
            if (String.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(lastName))
            {
                throw new Exception("Cannot be empty!");
            }
            RegistrationStatus registration = RegistrationStatus.Success;

            if (password != repeatPassword)
            {
               return RegistrationStatus.PasswordDoNotMatch;
            }

            User user = _unitOfWork.Users.GetAll().FirstOrDefault(s => s.Username == username);
            if (user != null)
            {
                return RegistrationStatus.UsernameAlreadyExists;
            }

            string hashedPassword = _hasher.HashPassword(password);
            
            User newUser = new User()
            {
                Name = name,
                LastName = lastName,
                Username = username,
                PasswordHash = hashedPassword
            };
            var createdUser = _unitOfWork.Users.Create(newUser);
            if (createdUser is null)
            {
                throw new Exception("Created user is null");
            }

            return registration;
        }

        public ChangePasswordStatus ChangePassword(string username, string password, string oldPassword)
        {
            ChangePasswordStatus status = ChangePasswordStatus.Success;

            var user = _unitOfWork.Users.GetAll().FirstOrDefault(s => s.Username == username);
            if (user is null)
            {
                status = ChangePasswordStatus.Failed;
                return status;
            }
            var passwordsMatch = _hasher.VerifyHashedPassword(user.PasswordHash, oldPassword);
            if (passwordsMatch == PasswordVerificationResult.Failed)
            {
                status = ChangePasswordStatus.Failed;
            }

            var newHash = _hasher.HashPassword(password);
            user.PasswordHash = newHash;
            var success = _unitOfWork.Users.Update(user.Id, user);
            if (!success)
            {
                throw new Exception("User update not successful");
            }


            return status;
        }
    }
}
