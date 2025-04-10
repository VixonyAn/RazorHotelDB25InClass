using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using RazorHotelDB25InClass.Services;

namespace RazorHotelDB25InClass.Pages.Hotels
{
    public class UpdateModel : PageModel
    {
        #region Instance Fields
        private IHotelService _hotelService;
        #endregion

        #region Properties
        [BindProperty] public Hotel Hotel { get; set; }
        #endregion

        #region Constructors
        public UpdateModel(IHotelService hotelService) // dependency injection
        {
            _hotelService = hotelService; // parameter overført
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion når Update Hotel siden bliver indlæst
        /// </summary>
        /// <param name="HotelNr">ID på Hotellet der skal opdateres</param>
        /// <returns>Hotellets informationer</returns>
        public async Task<IActionResult> OnGetAsync(int HotelNr)
        {
            Hotel = await _hotelService.GetHotelFromIdAsync(HotelNr); // henter hotel data
            return Page();
        }

        /// <summary>
        /// Funktion når "Update" klikkes på Update Hotel siden
        /// </summary>
        /// <param name="HotelNr">ID på Hotellet der skal opdateres</param>
        /// <returns>
        /// True: Hotel bliver opdateret og brugeren sendt tilbage til GetAllHotels
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindlæses
        /// </returns>
        public async Task<IActionResult> OnPostAsync(int HotelNr)
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                await _hotelService.UpdateHotelAsync(new Hotel(HotelNr, Hotel.Navn, Hotel.Adresse), HotelNr);
                return RedirectToPage("GetAllHotels");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
        #endregion
    }
}
