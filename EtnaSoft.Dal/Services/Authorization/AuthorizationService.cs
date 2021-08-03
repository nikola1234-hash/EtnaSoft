
using System;
using System.Linq;
using System.Text;
using EtnaSoft.Bo.Entities;
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

            PasswordVerificationResult result = _hasher.VerifyHashedPassword(user.PasswordHash, password);
            if (result == PasswordVerificationResult.Success)
            {
                return user;
            }

            if (result == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid Passowrd");
            }
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
            
            
            return registration;
        }
    }
}
