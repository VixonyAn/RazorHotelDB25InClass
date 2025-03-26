using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Pages.Hotels
{
    public class CreateModel : PageModel
    {
        #region Instance Fields
        private IHotelService _hotelService;
        #endregion

        #region Properties
        [BindProperty] // Two way binding
        public Hotel Hotel { get; set; }
        public string MessageError { get; set; }
        #endregion

        #region Constructor
        public CreateModel(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }
        #endregion

        #region Methods
        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _hotelService.CreateHotelAsync(Hotel);
                return RedirectToPage("GetAllHotels");
            }
            catch
            {
                MessageError = $"Kan ikke oprette en hotel med HotelNo '{Hotel.HotelNr}' denne ID findes allerede";
                return Page();
            }
        }
        #endregion
    }
}