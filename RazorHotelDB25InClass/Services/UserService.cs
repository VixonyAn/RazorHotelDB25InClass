using Microsoft.Data.SqlClient;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using System.Data;

namespace RazorHotelDB25InClass.Services
{
    public class UserService : Secret, IUserService
    {
        private string connectionString = Secret.ConnectionString;
        private string queryString = "Select Username, Password from Users";
        private string insertSql = "Insert into Users Values(@Username, @Password)";

        public async Task<List<User>> GetAllUsersAsync()
        {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync())
                    {
                        string username = reader.GetString("Username");
                        string password = reader.GetString("Password");
                        User user = new User(username, password);
                        users.Add(user);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
            }
            return users;
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(insertSql, connection);
                    command.Parameters.AddWithValue("@Username", user.Username);
                    command.Parameters.AddWithValue("@Password", user.Password);
                    await command.Connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
                return true;
            }
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                User user = new User();
                List<Hotel> hoteller = new List<Hotel>();
                try
                {
                    SqlCommand command = new SqlCommand(queryString + " where Username like @Search", connection);
                    command.Parameters.AddWithValue("@Search", "%" + username + "%");
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) // reads from data not from console
                    {
                        string uName = reader.GetString("Username");
                        string pWord = reader.GetString("Password");
                        user = new User(uName, pWord);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
                return user;
            }
        }

        public async Task<User?> VerifyUserAsync(string username, string password)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                User user = null;
                try
                {
                    SqlCommand command = new SqlCommand(queryString + " where Username = @Username and Password = @Password", connection);
                    command.Parameters.AddWithValue("@Username", username);
                    command.Parameters.AddWithValue("@Password", password);
                    await command.Connection.OpenAsync();
                    SqlDataReader reader = await command.ExecuteReaderAsync(); // breaks here
                    if (await reader.ReadAsync())
                    {
                        string uName = reader.GetString("Username");
                        string pWord = reader.GetString("Password");
                        user = new User(uName, pWord);
                    }
                    reader.Close();
                }
                catch (SqlException sqlExp)
                {
                    Console.WriteLine("Database error" + sqlExp.Message);
                    throw sqlExp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Generel fejl: " + ex.Message);
                    throw ex;
                }
                finally { }
                return user;
            }
        }
    }
}
