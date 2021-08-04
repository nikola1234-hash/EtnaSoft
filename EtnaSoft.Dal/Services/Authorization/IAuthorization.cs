using System;
using System.Collections.Generic;
using System.Text;
using EtnaSoft.Bo.Entities;

namespace EtnaSoft.Dal.Services.Authorization
{
    public enum RegistrationStatus
    {
        Success,
        PasswordDoNotMatch,
        UsernameAlreadyExists
    }
    public interface IAuthorization
    {
        User LoginUser(string username, string password);

        RegistrationStatus RegisterUser(string name, string lastName, string username, string password,
            string repeatPassword);


    }
}
