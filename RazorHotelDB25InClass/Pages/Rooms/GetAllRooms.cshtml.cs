using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using RazorHotelDB25InClass.Services;

namespace RazorHotelDB25InClass.Pages.Rooms
{
    public class GetAllRoomsModel : PageModel
    {
        #region Instance Fields
        private IRoomService _roomService;
        private IHotelService _hotelService;
        #endregion

        #region Properties
        public List<Room> Rooms { get; set; }

        [BindProperty]
        public int HotelNr { get; set; }

        public Hotel Hotel { get; set; }
        #endregion

        #region Constructor
        public GetAllRoomsModel(IRoomService roomService, IHotelService hotelService)
        {
            _roomService = roomService;
            _hotelService = hotelService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync(int hotelNr)
        {
            HotelNr = hotelNr;
            Hotel = await _hotelService.GetHotelFromIdAsync(hotelNr);
            Rooms = await _roomService.GetAllRoomAsync(HotelNr);
        }
        #endregion
    }
}
