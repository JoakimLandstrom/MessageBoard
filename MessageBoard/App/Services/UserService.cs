using System.Linq;
using App.DataAccess;
using App.Models;

namespace App.Services
{
    public interface IUserService
    {
        bool Authenticate(User user);
    }
    public class UserService : IUserService
    {
        private readonly IModelCache _cache;

        public UserService(IModelCache cache)
        {
            _cache = cache;
        }

        public bool Authenticate(User user)
        {
            var existingUser = _cache.GetAll<User>().SingleOrDefault(x => x.Username.Equals(user.Username));
            if (existingUser != null)
                return existingUser.Password.Equals(user.Password);

            _cache.Create(user);

            return true;
        }
    }
}