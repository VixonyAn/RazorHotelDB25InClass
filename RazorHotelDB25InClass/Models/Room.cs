using System.ComponentModel.DataAnnotations;

namespace RazorHotelDB25InClass.Models
{
    public class Room
    {
        #region Properties
        [Required(ErrorMessage = "RoomNr is required")]
        public int RoomNr { get; set; }

        [Required(ErrorMessage = "Type is required")]
        public char Types { get; set; }

        [Required(ErrorMessage = "Price is required")]
        public double Pris { get; set; }
        
        [Required(ErrorMessage = "HotelNr is required")]
        public int HotelNr { get; set; }
        #endregion

        public Room()
        {

        }
        public Room(int nr, char types, double pris)
        {
            RoomNr = nr;
            Types = types;
            Pris = pris;
        }

        public Room(int nr, char types, double pris, int hotelNr) : this(nr, types, pris)
        {
            HotelNr = hotelNr;
        }

        public override string ToString()
        {
            return $"Room = {RoomNr}, Types = {Types}, Pris = {Pris}";
        }
    }
}