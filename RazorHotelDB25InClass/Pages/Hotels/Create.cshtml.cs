using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using RazorHotelDB25InClass.Services;
using System.ComponentModel.DataAnnotations;

namespace RazorHotelDB25InClass.Pages.Hotels
{
    public class CreateModel : PageModel
    {
        #region Instance Fields
        private IHotelService _hotelService;
        #endregion

        #region Properties
        [BindProperty] public Hotel Hotel { get; set; }
        #endregion

        #region Constructor
        public CreateModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        #endregion

        #region Methods
        public void OnGet() { }

        /// <summary>
        /// Funktion når "Confirm" klikkes på Create Hotel siden
        /// </summary>
        /// <returns>
        /// True: Hotel bliver oprettet og brugeren sendt tilbage til GetAllHotels
        /// <br></br>
        /// False: ErrorMessage bliver aktiveret og siden genindlæses
        /// </returns>
        public async Task<IActionResult> OnPostAsync()
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                await _hotelService.CreateHotelAsync(Hotel);
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