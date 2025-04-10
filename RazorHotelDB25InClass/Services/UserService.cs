using Microsoft.Data.SqlClient;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using System.Data;

namespace RazorHotelDB25InClass.Services
{
    public class UserService : Secret, IUserService
    {
        private string connectionString = Secret.ConnectionString;
        private string queryString = "Select Username, Password, ImageUrl from Users";
        private string insertSql = "Insert into Users Values(@Username, @Password, @ImageUrl)";
        private string deleteSql = "Delete from Users where Username = @Username";

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
                        string imageUrl = reader.GetString("ImageUrl");
                        User user = new User(username, password, imageUrl);
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
                    command.Parameters.AddWithValue("@ImageUrl", user.ImageUrl);
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
                        string imgUrl = reader.GetString("ImageUrl");
                        user = new User(uName, pWord, imgUrl);
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
                    SqlDataReader reader = await command.ExecuteReaderAsync(); // breaks here - if secret string is incorrect
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

        public async Task<User?> DeleteUserAsync(string username)
        {
            User? user = GetUserByUsernameAsync(username).Result;
            if (user == null) { return null; }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand command = new SqlCommand(deleteSql, connection);
                    command.Parameters.AddWithValue("@Username", username);
                    await connection.OpenAsync();
                    int noOfRows = await command.ExecuteNonQueryAsync();
                    if (noOfRows == 0) { return null; }
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
