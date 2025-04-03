using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

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
        public async Task<IActionResult> OnGetAsync(int HotelNr)
        {
            Hotel = await _hotelService.GetHotelFromIdAsync(HotelNr); // henter hotel data
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int HotelNr)
        { // does not need validation as the values being changed do not have to be unique (they aren't primary keys)
            await _hotelService.UpdateHotelAsync(new Hotel(HotelNr, Hotel.Navn, Hotel.Adresse), HotelNr);
            return RedirectToPage("GetAllHotels");
        }
        #endregion
    }
}
