using System;
using System.Collections.Generic;
using App.DataAccess;
using App.Models;
using Xunit;

namespace Tests.IntegrationTests
{
    public class ModelCacheTests
    {
        private readonly ModelCache _cache;

        public ModelCacheTests()
        {
            _cache = new ModelCache();
        }

        [Fact]
        public void Create_MessageCreated()
        {
            var created = _cache.Create(new Message{Value = "asd"});
            var inCache = _cache.Get<Message>(created.Id);

            Assert.Equal(created, inCache);
        }

        [Fact]
        public void Update_CreatedMessageUpdated()
        {
            var created = _cache.Create(new Message {Value = "created"});
            created.Value = "updated message";
            
            var updated = _cache.Update(created);
            var inCache = _cache.Get<Message>(created.Id);
            
            Assert.Equal(updated, inCache);
        }

        [Fact]
        public void Delete_CreatedMessageDeleted()
        { 
            var created = _cache.Create(new Message {Value = "created"});
            
            _cache.Delete<Message>(created.Id);

            Assert.Throws<KeyNotFoundException>(() => _cache.Get<Message>(created.Id));
        }

        [Fact]
        public void GetAll_GetAllCreatedMessages()
        {
            var created1 = _cache.Create(new Message {Value = "created1"});
            var created2 = _cache.Create(new Message {Value = "created2"});
            var created3 = _cache.Create(new Message {Value = "created3"});

            var list = new List<Message>{created1, created2,created3};

            var result = _cache.GetAll<Message>();
            
            Assert.Equal(list, result);
        }
    }
}