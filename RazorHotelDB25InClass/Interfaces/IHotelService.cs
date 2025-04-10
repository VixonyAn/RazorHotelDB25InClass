using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Interfaces
{
    public interface IHotelService
    {
        /// <summary>
        /// Henter alle hoteller fra databasen og returnerer som liste
        /// </summary>
        /// <returns>Liste af Hoteller</returns>
        Task<List<Hotel>> GetAllHotelAsync();

        /// <summary>
        /// Henter et specifik hotel fra databasen
        /// </summary>
        /// <param name="hotelNr">HotelId man søger efter</param>
        /// <returns>Hotel objekt</returns>
        Task<Hotel?> GetHotelFromIdAsync(int hotelNr);

        /// <summary>
        /// Indsætter et nyt hotel i databasen
        /// </summary>
        /// <param name="hotel">Hotel objekt</param>
        /// <returns>True hvis Hotellet var oprettet</returns>
        Task<bool> CreateHotelAsync(Hotel hotel);

        /// <summary>
        /// Opdaterer en hotel i databasen
        /// </summary>
        /// <param name="hotel">Ny Hotel objekt</param>
        /// <param name="hotelNr">HotelId af Hotel der bliver ændret</param>
        /// <returns>True hvis Hotellet var opdateret</returns>
        Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr);

        /// <summary>
        /// Sletter et hotel fra databasen
        /// </summary>
        /// <param name="hotelNr">HotelId af Hotel man vil slette</param>
        /// <returns>Hotel objekt</returns>
        Task<Hotel?> DeleteHotelAsync(int hotelNr);

        /// <summary>
        /// Henter hoteller fra databasen hvis navn indeholder søgeordet og returnerer som liste
        /// </summary>
        /// <param name="name">Søgeord</param>
        /// <returns>Liste af Hoteller hvis navne indeholde søgeordet</returns>
        Task<List<Hotel>> GetHotelsByNameAsync(string name);
    }
}
