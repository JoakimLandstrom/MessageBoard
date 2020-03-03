using System.Collections.Generic;
using App.DataAccess;
using App.Models;
using App.Services;
using Moq;
using Xunit;

namespace Tests.ServiceTests
{
    public class UserServiceTests
    {
        private IUserService _service;
        private Mock<IModelCache> _mock;
        
        [Fact]
        public void Authenticate_NewUser_Authenticated()
        {
            _mock = new Mock<IModelCache>();

            var newUser = new User{Username = "test user", Password = "test password"};
            
            _mock.Setup(x => x.GetAll<User>()).Returns(new List<User>()).Verifiable();
            _mock.Setup(x => x.Create(newUser)).Returns(newUser).Verifiable();
            
            _service = new UserService(_mock.Object);
            
            Assert.True(_service.Authenticate(newUser));
            
            _mock.Verify();
            _mock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public void Authenticate_ExistingUser_Authenticated()
        {
            _mock = new Mock<IModelCache>();

            var existingUser = new User{Username = "test user", Password = "test password"};
            var user = new User{Username = "test user", Password = "test password"};
            
            _mock.Setup(x => x.GetAll<User>()).Returns(new List<User>{existingUser}).Verifiable();
            
            _service = new UserService(_mock.Object);
            
            Assert.True(_service.Authenticate(user));
            
            _mock.Verify();
            _mock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public void Authenticate_ExistingUser_NotAuthorized()
        {
            _mock = new Mock<IModelCache>();

            var existingUser = new User{Username = "test user", Password = "test password"};
            var user = new User{Username = "test user", Password = "wrong password"};

            
            _mock.Setup(x => x.GetAll<User>()).Returns(new List<User>{existingUser}).Verifiable();
            
            _service = new UserService(_mock.Object);
            
            Assert.False(_service.Authenticate(user));
            
            _mock.Verify();
            _mock.VerifyNoOtherCalls();
        }
    }
}