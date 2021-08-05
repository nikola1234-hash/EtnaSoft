using System;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Services.Authorization;

namespace EtnaSoft.WPF.Services.Authentication
{
    public class Authenticator : IAuthenticator
    {
        private readonly IAuthorization _authorization;

        public Authenticator(IAuthorization authorization)
        {
            _authorization = authorization;
        }


        public User CurrentUser { get; private set; }
        public bool IsLoggedIn => CurrentUser != null;
        public bool Login(string username, string password)
        {
            bool success = false;
            try
            {
                CurrentUser = _authorization.LoginUser(username, password);
                success = true;
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
