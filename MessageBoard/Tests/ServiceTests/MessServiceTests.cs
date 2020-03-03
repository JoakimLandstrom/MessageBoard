using System;
using App.DataAccess;
using App.Models;
using App.Services;
using Moq;
using Xunit;

namespace Tests.ServiceTests
{
    public class MessServiceTests
    {

        private Mock<IModelCache> _mock;
        private IMessageService _service;

        [Fact]
        public void UpdateMessage_MustBeSameUser_Success()
        {
            const string user = "test user";
            var key = Guid.NewGuid();
            
            var existingMessage = new Message
            {
                Id = key,
                Value   = "test message",
                User = user
            };
            
            var updatedMessage = new Message
            {
                Id = key,
                Value   = "updated test message",
                User = user
            };
            _mock = new Mock<IModelCache>();
            _mock.Setup(x => x.Get<Message>(key)).Returns(existingMessage).Verifiable();
            _mock.Setup(x => x.Update(updatedMessage)).Returns(updatedMessage).Verifiable();
            
            _service = new MessageService(_mock.Object);

            var result = _service.Update(updatedMessage, user);

            Assert.Equal(updatedMessage, result);
            
            _mock.Verify();
            _mock.VerifyNoOtherCalls();
        }


        [Fact]
        public void UpdateMessage_MustBeSameUser_Fail()
        {
            
            const string user = "test user";
            var key = Guid.NewGuid();
            
            var existingMessage = new Message
            {
                Id = key,
                Value   = "test message",
                User = user
            };
            
            var updatedMessage = new Message
            {
                Id = key,
                Value   = "updated test message",
                User = user
            };
            _mock = new Mock<IModelCache>();
            _mock.Setup(x => x.Get<Message>(key)).Returns(existingMessage).Verifiable();
            
            _service = new MessageService(_mock.Object);

           Assert.Throws<ApplicationException>(() => _service.Update(updatedMessage, "not the same user"));
           _mock.Verify();
           _mock.VerifyNoOtherCalls();
        }
        
        [Fact]
        public void DeleteMessage_MustBeSameUser_Success()
        {

            var key = Guid.NewGuid();
            const string expectedUser = "test user";
            
            var existingMessage = new Message{User = expectedUser};
            
            _mock = new Mock<IModelCache>();
            _mock.Setup(x => x.Get<Message>(key)).Returns(existingMessage).Verifiable();
            _mock.Setup(x => x.Delete<Message>(key)).Verifiable();
            
            _service = new MessageService(_mock.Object);
            
            _service.Delete(key, expectedUser);

            _mock.Verify();
            _mock.VerifyNoOtherCalls();
        }

        [Fact]
        public void DeleteMessage_MustBeSameUser_Fail()
        {

            var key = Guid.NewGuid();
            const string expectedUser = "test user";
            const string notTheSameUser = "not the same user";
            
            var existingMessage = new Message{User = expectedUser};
            
            _mock = new Mock<IModelCache>();
            _mock.Setup(x => x.Get<Message>(key)).Returns(existingMessage).Verifiable();
            
            _service = new MessageService(_mock.Object);

            Assert.Throws<ApplicationException>(() => _service.Delete(key, notTheSameUser));
            
            _mock.Verify();
            _mock.VerifyNoOtherCalls();
        }
    }
}