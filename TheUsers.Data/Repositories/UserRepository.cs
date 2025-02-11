using TheUsers.Domain.Models;
using TheUsers.Domain.Repositories;

namespace TheUsers.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private List<User> _users;
        public UserRepository()
        {
            // fake temporal data, in memory collection.
            _users = new List<User>();
            _users.Add(new User { Id = 1, FirstName = "John", LastName = "Doe", Email = "johndoe@email.com", DateOfBirth = new DateTime(1980, 1, 1), PhoneNumber = 1234567890 });
            _users.Add(new User { Id = 2, FirstName = "Jane", LastName = "Doe", Email = "janedoe@email.com", DateOfBirth = new DateTime(1980, 1, 1), PhoneNumber = 1234567890 });
        }
        public void Add(User user)
        {
            _users.Add(user);
        }

        public void Delete(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
            }
        }

        public User GetById(int id)
        {
            var user = _users.FirstOrDefault(u => u.Id == id);
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users;
        }

        public void Update(User user)
        {
            var existingUser = _users.FirstOrDefault(u => u.Id == user.Id);
            if (existingUser != null)
            {
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.DateOfBirth = user.DateOfBirth;
                existingUser.PhoneNumber = user.PhoneNumber;
            }
        }
    }
}
