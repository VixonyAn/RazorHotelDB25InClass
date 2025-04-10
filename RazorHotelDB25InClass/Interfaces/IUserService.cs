using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Interfaces
{
    public interface IUserService
    {
        // Henter alle users fra databasen og returnerer som liste
        Task<List<User>> GetAllUsersAsync();

        // Henter en specifik user fra databasen
        Task<User> GetUserByUsernameAsync(string username);

        // Indsætter en nyt user i databasen
        Task<bool> CreateUserAsync(User newUser);

        // Verificerer user eksisterer til login
        Task<User?> VerifyUserAsync(string username, string password);

        // Sletter en user fra databasen
        Task<User?> DeleteUserAsync(string username);
    }
}
