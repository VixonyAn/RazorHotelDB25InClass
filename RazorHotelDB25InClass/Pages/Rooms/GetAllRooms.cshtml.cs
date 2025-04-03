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

        [BindProperty]
        public int HotelNr { get; set; }

        public Hotel Hotel { get; set; }
        public List<SelectListItem> HotelSelectList { get; set; }
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
            // onget if hotelNr = null then set to get all?
        }

        //private async Task createHotelSelectList() // small first letter for private methods
        //{
        //    List<Hotel> HotelList = await _hotelService.GetAllHotelAsync();
        //    HotelSelectList = new List<SelectListItem>();
        //    HotelSelectList.Add(new SelectListItem("Vælg en Hotel", "-1"));
        //    foreach (Hotel hotel in HotelList)
        //    {
        //        SelectListItem hsl = new SelectListItem(hotel.HotelNr.ToString(), hotel.Navn);
        //        HotelSelectList.Add(hsl);
        //    }
        //}
        #endregion
    }
}
