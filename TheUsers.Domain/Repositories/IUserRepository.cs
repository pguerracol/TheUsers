using TheUsers.Domain.Models;

namespace TheUsers.Domain.Repositories
{
    public interface IUserRepository
    {
        //Task<IEnumerable<User>> GetUsers();
        //Task<User> GetUser(int id);
        //Task<User> AddUser(User user);
        //Task<User> UpdateUser(User user);
        //Task<User> DeleteUser(int id);

        IEnumerable<User> GetAll();
        User GetById(int id);
        void Add(User user);
        void Update(User user);
        void Delete(int id);
    }
}
