using System;
using System.Collections.Generic;
using App.DataAccess;
using App.Models;

namespace App.Services
{
    public interface IMessageService
    {
        IEnumerable<Message> GetAll();
        Message Update(Message message, string user);
        Message Create(Message message, string user);
        void Delete(Guid guid, string user);

    }

    public class MessageService : IMessageService
    {
        private readonly IModelCache _cache;

        public MessageService(IModelCache cache)
        {
            _cache = cache;
        }

        public IEnumerable<Message> GetAll()
        {
            return _cache.GetAll<Message>();
        }

        public Message Create(Message message, string user)
        {
            message.Created = DateTime.Now;
            message.User = user;
            return _cache.Create(message);
        }
        
        public Message Update(Message message, string user)
        {
            var existingMessage = _cache.Get<Message>(message.Id);
            
            if(!existingMessage.User.Equals(user))
                throw new ApplicationException("You cannot update somebody else message");
            
            return _cache.Update(message);
        }

        public void Delete(Guid guid, string user)
        {
            var message = _cache.Get<Message>(guid);

            if (!message.User.Equals(user)) 
                throw new ApplicationException("You cannot delete somebody else message");
            
            _cache.Delete<Message>(guid);
        }
    }
}