using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Interfaces
{
    public interface IHotelService
    {
        /// henter alle hoteller fra databasen
        /// <returns>Liste af hoteller</returns>
        Task<List<Hotel>> GetAllHotelAsync();

        /// Henter et specifik hotel fra database 
        /// <param name="hotelNr">Udpeger det hotel der ønskes fra databasen</param>
        /// <returns>Det fundne hotel eller null hvis hotellet ikke findes</returns>
        Task<Hotel?> GetHotelFromIdAsync(int hotelNr);

        /// Indsætter et nyt hotel i databasen
        /// <param name="hotel">hotellet der skal indsættes</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> CreateHotelAsync(Hotel hotel);

        /// Opdaterer en hotel i databasen
        /// <param name="hotel">De nye værdier til hotellet</param>
        /// <param name="hotelNr">Nummer på den hotel der skal opdateres</param>
        /// <returns>Sand hvis der er gået godt ellers falsk</returns>
        Task<bool> UpdateHotelAsync(Hotel hotel, int hotelNr);

        /// Sletter et hotel fra databasen
        /// <param name="hotelNr">Nummer på det hotel der skal slettes</param>
        /// <returns>Det hotel der er slettet fra databasen, returnere null hvis hotellet ikke findes</returns>
        Task<Hotel?> DeleteHotelAsync(int hotelNr);

        /// henter alle hoteller fra databasen
        /// <param name="name">Angiver navn på hotel der hentes fra databasen</param>
        /// <returns></returns>
        Task<List<Hotel>> GetHotelsByNameAsync(string name);
    }
}
