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

            /* if statement
            if (_hotelService.GetHotelFromIdAsync(Hotel.HotelNr) != null)
            {
                if (ModelState.IsValid)
                {
                    MessageError = $"Cannot create hotel. HotelID is already in use in the system.";
                }
                return Page();
            }
            await _hotelService.CreateHotelAsync(Hotel);
            return RedirectToPage("GetAllHotels");
            */
            /* try catch statement
            try
            {
                await _hotelService.CreateHotelAsync(Hotel);
                return RedirectToPage("GetAllHotels");
            }
            catch
            {
                MessageError = $"Kan ikke oprette en hotel med HotelNo '{Hotel.HotelNr}' denne ID findes allerede";
                return Page();
            } */
        }
        #endregion
    }
}