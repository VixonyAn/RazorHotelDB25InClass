using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Pages.Hotels
{
    public class DeleteModel : PageModel
    {
        #region Instance Fields
        private IHotelService _hotelService;
        #endregion

        #region Properties
        [BindProperty] public Hotel Hotel { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string MessageError { get; set; }
        #endregion

        #region Constructors
        public DeleteModel(IHotelService hotelService) // dependency injection
        {
            _hotelService = hotelService; // parameter overf�rt
        }
        #endregion

        #region Methods
        /// <summary>
        /// Funktion n�r Delete Hotel siden bliver indl�st
        /// </summary>
        /// <param name="HotelNr">ID p� Hotellet der skal slettes</param>
        /// <returns>Hotellets informationer</returns>
        public async Task<IActionResult> OnGetAsync(int HotelNr)
        {
            Hotel = await _hotelService.GetHotelFromIdAsync(HotelNr); // henter hotel data
            return Page();
        }

        /// <summary>
        /// Funktion n�r "Delete" klikkes p� Delete Hotel siden
        /// </summary>
        /// <param name="HotelNr">ID p� Hotellet der skal slettes</param>
        /// <returns>
        /// True: Hotel bliver slettet og brugeren sendt tilbage til GetAllHotels
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindl�ses
        /// </returns>
        public async Task<IActionResult> OnPostAsync(int HotelNr)
        {
            if (Confirm == false)
            {
                MessageError = $"Remember to check the Confirm box";
                return Page();
            }
            try
            {
                await _hotelService.DeleteHotelAsync(HotelNr);
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
