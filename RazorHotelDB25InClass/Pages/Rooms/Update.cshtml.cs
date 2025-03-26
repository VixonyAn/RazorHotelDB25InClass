using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Pages.Rooms
{
    public class UpdateModel : PageModel
    {
        #region Instance Fields
        private IRoomService _roomService;
        #endregion

        #region Properties
        [BindProperty] // Two way binding
        public Room Room { get; set; }

        [BindProperty]
        public int HotelNr { get; set; }
        
        [BindProperty]
        public int RoomNr { get; set; }

        public string MessageError { get; set; }
        #endregion

        #region Constructors
        public UpdateModel(IRoomService roomService) // dependency injection
        {
            this._roomService = roomService; // parameter overført
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
            await _roomService.UpdateRoomAsync(Room, RoomNr, HotelNr);
            return RedirectToPage("GetAllRooms", new { HotelNr = HotelNr });
        }
        #endregion
    }
}
