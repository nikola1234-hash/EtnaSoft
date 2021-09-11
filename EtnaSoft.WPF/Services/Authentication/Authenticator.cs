using System;

using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal;
using EtnaSoft.Dal.Services.Authorization;
using EtnaSoft.Dal.Stores;

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
            CurrentUser = _authorization.LoginUser(username, password);
            if (CurrentUser != null)
            {
                UserStore.CurrentUser = CurrentUser.Username;
                success = true;
            }

            return success;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
