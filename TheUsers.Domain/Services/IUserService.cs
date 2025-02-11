using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheUsers.Domain.Models;

namespace TheUsers.Domain.Services
{
    public interface IUserService
    {
        //Task<IEnumerable<User>> GetAllUsers();
        //Task<User> GetUserById(int id);
        //Task<User> AddUser(User user);
        //Task<User> UpdateUser(User user);
        //Task<User> DeleteUser(int id);
        IEnumerable<User> GetAllUsers();
        User GetUserById(int id);
        void AddUser(User user);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
