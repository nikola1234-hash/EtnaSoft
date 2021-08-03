using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EtnaSoft.Bo.Entities;
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

    }
}
