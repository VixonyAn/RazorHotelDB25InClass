using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Interfaces
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<bool> CreateUserAsync(User newUser);
        Task<User> GetUserByUsernameAsync(string username);
        Task<User?> VerifyUserAsync(string username, string password);
    }
}
