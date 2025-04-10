using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Interfaces
{
    public interface IRoomService
    {
        // Henter alle værelser til et hotel fra databasen og returnerer som liste
        Task<List<Room>> GetAllRoomAsync(int hotelNr);

        // Henter et specifik værelse fra databasen
        Task<Room?> GetRoomFromIdAsync(int roomNr, int hotelNr);

        // Indsætter et nyt værelse i databasen
        Task<bool> CreateRoomAsync(int hotelNr, Room room);

        // Opdaterer en værelse i databasen
        Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr);

        // Sletter et værelse fra databasen
        Task<Room?> DeleteRoomAsync(int roomNr, int hotelNr);
    }
}