using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;
using RazorHotelDB25InClass.Services;
using System.Reflection;

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
        [BindProperty] public int HotelNr { get; set; }
        public Hotel Hotel { get; set; }
        public string MessageError { get; set; }
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
            try
            {
                // if an invalid hotelNr is received, the hotelNr is set to "1"
                if (await _hotelService.GetHotelFromIdAsync(hotelNr) == null )
                {
                    hotelNr = 1;
                    MessageError = $"Invalid hotelNr received, redirected to hotelNr 1";
                }
                // NB: this can cause issues if the database does not contain a hotel with this ID
                HotelNr = hotelNr;
                Hotel = await _hotelService.GetHotelFromIdAsync(hotelNr);
                Rooms = await _roomService.GetAllRoomAsync(HotelNr);
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
        }
        #endregion
    }
}
