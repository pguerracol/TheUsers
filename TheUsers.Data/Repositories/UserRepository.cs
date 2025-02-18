using Microsoft.Data.SqlClient;
using System.Data;
using TheUsers.Domain.Models;
using TheUsers.Domain.Repositories;

namespace TheUsers.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly string strConn;

        public UserRepository()
        {
            strConn = "Data Source=(local)\\SQLEXPRESS;Initial Catalog=Users;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;";
        }

        public IEnumerable<User> GetAll()
        {
            var _allUsers = new List<User>();
            try
            {
                using (var conn = new SqlConnection(strConn))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT [Id], " +
                                "[FirstName], [LastName], [Email], " +
                                "[DateOfBirth], [PhoneNumber] " +
                                "FROM [Users].[dbo].[Users]", conn);
                    cmd.CommandType = CommandType.Text;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _allUsers.Add(new User
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Email = reader["Email"].ToString(),
                                DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                PhoneNumber = Convert.ToInt32(reader["PhoneNumber"])
                            });
                        }
                    }
                }
                return _allUsers;

            }
            catch (Exception ex)
            {
                throw new Exception("Sql Exception.", ex);
            }
        }

        public User GetById(int id)
        {
            var user = new User();
            using (var conn = new SqlConnection(strConn))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand($"SELECT [Id], " +
                            "[FirstName], [LastName], [Email], " +
                            "[DateOfBirth], [PhoneNumber] " +
                            "FROM [Users].[dbo].[Users] " +
                            "WHERE [Id] = " + id, conn);
                cmd.CommandType = CommandType.Text;

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user.Id = Convert.ToInt32(reader["Id"]);
                        user.FirstName = reader["FirstName"].ToString();
                        user.LastName = reader["LastName"].ToString();
                        user.Email = reader["Email"].ToString();
                        user.DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                        user.PhoneNumber = Convert.ToInt32(reader["PhoneNumber"]);
                    }
                }
            }
            return user;

        }

        public void Add(User user)
        {
            using (var conn = new SqlConnection(strConn))
            {

                SqlCommand cmd = new SqlCommand("INSERT INTO [dbo].[Users]" +
                    "([FirstName],[LastName],[Email],[DateOfBirth],[PhoneNumber])" +
                    "VALUES('" + user.FirstName + "', '" + 
                    user.LastName + "', '" + 
                    user.Email + "', '" + 
                    user.DateOfBirth.ToString("yyyy-MM-dd") + "', " + 
                    user.PhoneNumber + ")", conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }

        public void Update(User user)
        {
            using (var conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand("UPDATE [dbo].[Users] " +
                    "SET [FirstName] = '" + user.FirstName + "', " +
                     "[LastName] = '" + user.LastName + "', " +
                     "[Email] = '" + user.Email + "', " +
                     "[DateOfBirth] = '" + user.DateOfBirth.ToString("yyyy-MM-dd") + "', " +
                     "[PhoneNumber] = " + user.PhoneNumber + " " +
                    "WHERE [Id] = " + +user.Id, conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }

        public void Delete(int id)
        {
            using (var conn = new SqlConnection(strConn))
            {
                SqlCommand cmd = new SqlCommand($"DELETE " +
                            "FROM [dbo].[Users] " +
                            "WHERE [Id] = " + id, conn);
                cmd.CommandType = CommandType.Text;
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
