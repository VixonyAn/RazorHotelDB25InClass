using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using RazorHotelDB25InClass.Services;

namespace RazorHotelDB25InClass.Pages.Rooms
{
    public class CreateModel : PageModel
    {
        #region Instance Fields
        private IRoomService _roomService;
        #endregion

        #region Properties
        [BindProperty] public Room Room { get; set; }
        [BindProperty] public int HotelNr { get; set; }
        [BindProperty] public string Types { get; set; }
        public string MessageError { get; set; }
        public List<SelectListItem> TypeSelectList { get; set; }
        #endregion

        #region Constructor
        public CreateModel(IRoomService roomService)
        {
            _roomService = roomService;
            createTypeSelectList();
        }
        #endregion

        #region Methods
        public async Task OnGetAsync(int hotelNr)
        {
            Room = new Room();
            Room.HotelNr = hotelNr;
            HotelNr = hotelNr;
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
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) { return Page(); }
            if (await _roomService.GetRoomFromIdAsync(Room.RoomNr, Room.HotelNr) != null)
            { // if combo of values return an object, combo of values cannot be used again (dupes clause)
                MessageError = $"Cannot create room. RoomID is already in use at this hotel.";
                return Page();
            }
            try
            {
                await _roomService.CreateRoomAsync(Room.HotelNr, new Room(Room.RoomNr, Types[0], Room.Pris, Room.HotelNr));
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