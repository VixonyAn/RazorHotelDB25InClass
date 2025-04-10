using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using RazorHotelDB25InClass.Services;

namespace RazorHotelDB25InClass.Pages.Rooms
{
    public class DeleteModel : PageModel
    {
        #region Instance Fields
        private IRoomService _roomService;
        #endregion

        #region Properties
        [BindProperty] public Room Room { get; set; }
        [BindProperty] public int HotelNr { get; set; }
        [BindProperty] public int RoomNr { get; set; }
        [BindProperty] public bool Confirm { get; set; }
        public string MessageError { get; set; }
        #endregion

        #region Constructors
        public DeleteModel(IRoomService roomService) // dependency injection
        {
            _roomService = roomService; // parameter overført
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync(int roomNr, int hotelNr)
        {
            HotelNr = hotelNr;
            RoomNr = roomNr;
            Room = await _roomService.GetRoomFromIdAsync(roomNr, hotelNr); // henter room data
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Confirm == false)
            {
                MessageError = $"Remember to check the Confirm box";
                return Page();
            }
            try
            {
                await _roomService.DeleteRoomAsync(RoomNr, HotelNr);
                return RedirectToPage("GetAllRooms", new { HotelNr = HotelNr });
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
