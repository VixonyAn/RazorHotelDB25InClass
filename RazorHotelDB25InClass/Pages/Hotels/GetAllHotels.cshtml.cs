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
        { // OnGet k�rer n�r siden indl�ses
            Hotels = await _hotelService.GetAllHotelAsync(); // fylder listen med data
        } // await l�ser den del af applikationen som afh�nger af dataen der ventes p�, mens resten af programmet kan blive ved med at k�re 
        #endregion
    }
}
