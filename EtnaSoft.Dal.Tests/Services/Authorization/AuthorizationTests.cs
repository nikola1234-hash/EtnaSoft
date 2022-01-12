using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ErtnaSoft.Bo.Entities;
using EtnaSoft.Dal.Exceptions;
using EtnaSoft.Dal.Infrastucture;
using EtnaSoft.Dal.Services;
using EtnaSoft.Dal.Services.Authorization;
using Microsoft.AspNet.Identity;
using Moq;
using Xunit;

namespace EtnaSoft.Dal.Tests.Services.Authorization
{
    public class AuthorizationTests
    {

        private readonly AuthorizationService _sut;

        private readonly Mock<IUnitOfWork> _unitMock = new Mock<IUnitOfWork>();
        private readonly Mock<IPasswordHasher> _passMock = new Mock<IPasswordHasher>();

        public AuthorizationTests()
        {
            _sut = new AuthorizationService(_unitMock.Object, _passMock.Object);
        }



        [Fact]
        public void LoginUser_IfUsernameIsEmptyOrWhiteSpace_ThrowException()
        {
            string username = "";
            string password = "134";

            Assert.Throws<Exception>(()=>_sut.LoginUser(username, password));

        }

        [Fact]
        public void LoginUser_IfPasswordIsEmptyOrWhiteSpace_ThrowsException()
        {
            string username = "124";
            string password = "";

            Assert.Throws<Exception>(()=>_sut.LoginUser(username, password));

        }

        [Fact]
        public void LoginUser_IfUserDoesNotExist_ThrowsException()
        {
            string username = "DoesNotExist";
            string password = "1234";
            IEnumerable<User> user;
            user = new List<User>()
            {
                new User(),
                new User()
            };

            _unitMock.Setup(s => s.Users.GetAll()).Returns(user);

            Assert.Throws<Exception>(() => _sut.LoginUser(username, password));

        }
        [Fact]
        public void LoginUser_IfPasswordHashDoNotMatch_ThrowsException()
        {
            string username = "Admin";
            string password = "123";
            List<User> users = new List<User>()
            {
                new User()
                {
                    Id= 1,
                    IsActive = true,
                    LastName = "Savic",
                    Name = "Nikola",
                    PasswordHash = It.IsAny<string>(),
                    Username = "Admin"
                    }
            };
            _unitMock.Setup(s => s.Users.GetAll()).Returns(users);
            _passMock.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Failed);

            Assert.Throws<Exception>(() => _sut.LoginUser(username, password));
        }

        [Fact]
        public void LoginUser_IfUserIsNotActive_ThrowsException()
        {
            string username = "Admin";
            string password = "123";
            List<User> users = new List<User>()
            {
                new User()
                {
                    Id= 1,
                    IsActive = false,
                    LastName = "Savic",
                    Name = "Nikola",
                    PasswordHash = It.IsAny<string>(),
                    Username = "Admin"
                }
            };
            _unitMock.Setup(s => s.Users.GetAll()).Returns(users);
            _passMock.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);
            Assert.Throws<UserNotActiveExeption>(() => _sut.LoginUser(username, password));
        }
        [Fact]
        public void LoginUser_IfAllIsValid_ShouldLogin()
        {
            string username = "Admin";
            string password = "123";
            List<User> users = new List<User>()
            {
                new User()
                {
                    Id = 1,
                    IsActive = true,
                    LastName = "Savic",
                    Name = "Nikola",
                    PasswordHash = It.IsAny<string>(),
                    Username = "Admin"
                }
            };
            User expectedUser = new User()
            {
                
                Id = 1,
                IsActive = true,
                LastName = "Savic",
                Name = "Nikola",
                PasswordHash = It.IsAny<string>(),
                Username = "Admin"
            };
            _unitMock.Setup(s => s.Users.GetAll()).Returns(users);
            _passMock.Setup(s => s.VerifyHashedPassword(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(PasswordVerificationResult.Success);
            User actualUser = _sut.LoginUser(username, password);

            Assert.Equal(expectedUser.Id, actualUser.Id);
            Assert.Equal(expectedUser.IsActive, actualUser.IsActive);
            Assert.Equal(expectedUser.LastName, actualUser.LastName);
            Assert.Equal(expectedUser.Name, actualUser.Name);
            Assert.Equal(expectedUser.PasswordHash, actualUser.PasswordHash);
            Assert.Equal(expectedUser.Username, actualUser.Username);


        }

    }
}
