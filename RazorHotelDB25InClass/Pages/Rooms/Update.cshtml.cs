using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
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
        [BindProperty] public Room Room { get; set; }
        [BindProperty] public int HotelNr { get; set; }
        [BindProperty] public int RoomNr { get; set; }
        [BindProperty] public string Types { get; set; }
        public string MessageError { get; set; }
        public List<SelectListItem> TypeSelectList { get; set; }
        #endregion

        #region Constructors
        public UpdateModel(IRoomService roomService) // dependency injection
        {
            _roomService = roomService; // parameter overført
            createTypeSelectList();
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

        private void createTypeSelectList() // small first letter for private methods
        {
            TypeSelectList = new List<SelectListItem>();
            TypeSelectList.Add(new SelectListItem("Select a type", "-1"));
            TypeSelectList.Add(new SelectListItem("S", "S"));
            TypeSelectList.Add(new SelectListItem("D", "D"));
            TypeSelectList.Add(new SelectListItem("F", "F"));
        }

        public async Task<IActionResult> OnPostAsync()
        { // need to validate that a type and price are selected
            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                await _roomService.UpdateRoomAsync(new Room(Room.RoomNr, Types[0], Room.Pris, Room.HotelNr), RoomNr, HotelNr);
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
