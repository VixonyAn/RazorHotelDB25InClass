using System.ComponentModel.DataAnnotations;

namespace RazorHotelDB25InClass.Models
{
    public class Hotel : IComparable<Hotel>
    {
        #region Properties
        [Required(ErrorMessage = "HotelNr is required")]
        public int HotelNr { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(30, ErrorMessage = "Exceeded character length of 30")]
        public String Navn { get; set; }
        
        [Required(ErrorMessage = "Adresse is required")]
        [StringLength(50, ErrorMessage = "Exceeded character length of 50")]
        public String Adresse { get; set; }
        #endregion

        /// <summary>
        /// Parameterløs constructor til Hotel
        /// </summary>
        public Hotel() { }

        /// <summary>
        /// Hotel constructor
        /// </summary>
        /// <param name="hotelNr">Primary key, unikt nummer som identificerer et Hotel</param>
        /// <param name="navn">Hotellets navn</param>
        /// <param name="adresse">Hotellets adresse</param>
        public Hotel(int hotelNr, string navn, string adresse)
        {
            HotelNr = hotelNr;
            Navn = navn;
            Adresse = adresse;
        }

        /// <summary>
        /// ToString for Hoteller
        /// </summary>
        /// <returns>String med Hotellets navn, id, og adresse</returns>
        public override string ToString()
        {
            return $"{nameof(HotelNr)}: {HotelNr}, {nameof(Navn)}: {Navn}, {nameof(Adresse)}: {Adresse}";
        }

        /// <summary>
        /// Sammenligner navne af hoteller for at organisere i alfabetisk rækkefølge
        /// </summary>
        /// <param name="other">Det andet hotel, som den første hotel bliver sammenlignet med</param>
        /// <returns>Returns -1, 0, eller 1</returns>
        public int CompareTo(Hotel? other)
        {
            return Navn.CompareTo(other.Navn);
        }
    }
}
