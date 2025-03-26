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
        [BindProperty] // Two way binding
        public Hotel Hotel { get; set; }

        [BindProperty]
        public bool Confirm { get; set; }

        public string MessageError { get; set; }
        #endregion

        #region Constructors
        public DeleteModel(IHotelService hotelService) // dependency injection
        {
            this._hotelService = hotelService; // parameter overført
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync(int HotelNr)
        {
            Hotel = await _hotelService.GetHotelFromIdAsync(HotelNr); // henter hotel data
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int HotelNr)
        {
            if (Confirm == false)
            {
                MessageError = $"Remember to check the Confirm box";
                return Page();
            }
            await _hotelService.DeleteHotelAsync(HotelNr);
            return RedirectToPage("GetAllHotels");
        }
        #endregion
    }
}
