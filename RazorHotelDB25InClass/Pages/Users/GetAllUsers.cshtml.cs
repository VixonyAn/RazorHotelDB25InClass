using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Pages.Users
{
    public class GetAllUsersModel : PageModel
    {
        #region Instance Fields
        private IUserService _userService;
        #endregion

        #region Properties
        [BindProperty] public User User { get; set; }
        public List<User> Users { get; set; }
        #endregion

        #region Constructors
        public GetAllUsersModel(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync()
        {
            try
            { 
                Users = await _userService.GetAllUsersAsync();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(string username)
        {
            await _userService.DeleteUserAsync(username);
            return RedirectToPage("GetAllUsers");
        }
        #endregion
    }
}
