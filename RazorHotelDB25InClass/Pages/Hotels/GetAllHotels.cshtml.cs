using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using RazorHotelDB25InClass.Helpers;

namespace RazorHotelDB25InClass.Pages.Hotels
{
    public class GetAllHotelsModel : PageModel
    {
        #region Instance Fields
        private IHotelService _hotelService;
        #endregion

        #region Properties
        public List<Hotel> Hotels { get; set; }
        [BindProperty (SupportsGet = true)] public string FilterCriteria { get; set; }
        [BindProperty(SupportsGet = true)] public string SortBy { get; set; }
        [BindProperty(SupportsGet = true)] public string SortOrder { get; set; }
        #endregion

        #region Constructor
        public GetAllHotelsModel(IHotelService hotelService)
        { // etablerer forbindelse til interface - dependency injection
            _hotelService = hotelService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync() // OnGet kører når siden indlæses
        { // await låser den del af applikationen som afhænger af dataen der ventes på, mens resten af programmet kan blive ved med at køre
            try
            {
                if (!String.IsNullOrEmpty(FilterCriteria))
                {
                    Hotels = await _hotelService.GetHotelsByNameAsync(FilterCriteria);
                }
                else
                {
                    Hotels = await _hotelService.GetAllHotelAsync(); // fylder listen med data
                }
                if (SortBy == "Navn") { Hotels.Sort(); }
                if (SortBy == "Adresse") { Hotels.Sort(new HotelAddressCompare()); }
                if (SortOrder == "Descending") { Hotels.Reverse(); }
            }
            catch (Exception ex)
            {
                Hotels = new List<Hotel>();
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
        #endregion
    }
}
