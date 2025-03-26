using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        #region Instance Fields
        private IHotelService _hotelService;
        #endregion

        #region Properties
        public List<Hotel> Hotels { get; set; }
        #endregion

        #region Constructor
        public GetAllHotelsModel(IHotelService hotelService)
        { // etablerer forbindelse til interface - dependency injection
            _hotelService = hotelService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync()
        { // OnGet kører når siden indlæses
            Hotels = await _hotelService.GetAllHotelAsync(); // fylder listen med data
        } // await låser den del af applikationen som afhænger af dataen der ventes på, mens resten af programmet kan blive ved med at køre 
        #endregion
    }
}
