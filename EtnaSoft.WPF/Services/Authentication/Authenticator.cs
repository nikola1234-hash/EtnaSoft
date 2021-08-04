using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Exceptions;
using EtnaSoft.Dal.Services;
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


        public User CurrentUser { get; set; }
        public bool IsLoggedIn => CurrentUser != null;
        public User Login(string username, string password)
        {
            var user = _authorization.LoginUser(username, password);
            CurrentUser = user;
            return user;
        }

        public void Logout()
        {
            CurrentUser = null;
        }
    }
}
