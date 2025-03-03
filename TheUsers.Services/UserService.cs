using TheUsers.Domain.Models;
using TheUsers.Domain.Models.Exceptions;
using TheUsers.Domain.Repositories;
using TheUsers.Domain.Services;

namespace TheUsers.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public void DeleteUser(int id)
        {
            _userRepository.Delete(id);
        }

        public IEnumerable<User> GetAllUsers()
        {
            var _users = _userRepository.GetAll();
            return _users.Select(u => new UserWithAge
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                DateOfBirth = u.DateOfBirth,
                PhoneNumber = u.PhoneNumber,
                Age = DateTime.Now.Year - u.DateOfBirth.Year - (DateTime.Now.DayOfYear < u.DateOfBirth.DayOfYear ? 1 : 0)
            }).ToList();
        }

        public User GetUserById(int id)
        {
            var user = _userRepository.GetById(id);

            if (user.FirstName == null)
            {
                throw new UserNotFoundException();
            }
            return new UserWithAge
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                PhoneNumber = user.PhoneNumber,
                Age = DateTime.Now.Year - user.DateOfBirth.Year - (DateTime.Now.DayOfYear < user.DateOfBirth.DayOfYear ? 1 : 0)
            };
        }

        public void UpdateUser(User user)
        {
            _userRepository.Update(user);
        }
    }
}
