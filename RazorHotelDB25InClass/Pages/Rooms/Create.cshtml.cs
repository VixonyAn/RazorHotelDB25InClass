using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        #region Instance Fields
        private IRoomService _roomService;
        #endregion

        #region Properties
        [BindProperty] // Two way binding
        public Room Room { get; set; }

        [BindProperty]
        public int HotelNr { get; set; }
        
        public string MessageError { get; set; }
        #endregion

        #region Constructor
        public CreateModel(IRoomService roomService)
        {
            _roomService = roomService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync(int hotelNr)
        {
            Room = new Room();
            Room.HotelNr = hotelNr;
            HotelNr = hotelNr;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                await _roomService.CreateRoomAsync(Room.HotelNr, Room);
                return RedirectToPage("GetAllRooms", new { HotelNr = HotelNr });
            }
            catch
            {
                MessageError = $"Kan ikke oprette en værelse. Enten findes HotelNr ikke, eller RoomNr er i brug på denne hotel allerede.";
                return Page();
            }
        }
        #endregion
    }
}