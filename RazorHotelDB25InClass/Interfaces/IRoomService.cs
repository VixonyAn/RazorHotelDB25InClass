using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Interfaces
{
    public interface IRoomService
    {
        /// henter alle værelser til et hotel fra databasen
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Liste af værelser</returns>
        Task<List<Room>> GetAllRoomAsync(int hotelNr);

        /// Henter et specifik værelse fra database 
        /// <param name="roomNr">Udpeger det værelse der ønskes fra databasen</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Den fundne værelse eller null hvis værelset ikke findes</returns>
        Task<Room?> GetRoomFromIdAsync(int roomNr, int hotelNr);

        /// Indsætter et ny værelse i databasen
        /// <param name="room">Værelset der skal indsættes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> CreateRoomAsync(int hotelNr, Room room);

        /// Opdaterer en værelse i databasen
        /// <param name="room">De nye værdier til værelset</param>
        /// <param name="roomNr">Nummer på det værelse der skal opdateres</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> UpdateRoomAsync(Room room, int roomNr, int hotelNr);

        /// Sletter et værelse fra databasen
        /// <param name="roomNr">Nummer på det værelse der skal slettes</param>
        /// <param name="hotelNr">Nummeret på hotellet</param>
        /// <returns>Det værelse der er slettet fra databasen, returnere null hvis værelset ikke findes</returns>
        Task<Room?> DeleteRoomAsync(int roomNr, int hotelNr);
    }
}