using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Pages.Users
{
    public class GetAllUsersModel : PageModel
    {
        #region Instance Fields
        private readonly ILogger<GetAllUsersModel> _logger;
        private IUserService _userService;
        #endregion

        #region Properties
        [BindProperty] public User User { get; set; }
        public List<User> Users { get; set; }
        public string Username { get; set; }
        public User CurrentUser { get; set; }
        #endregion

        #region Constructors
        public GetAllUsersModel(ILogger<GetAllUsersModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }
        #endregion

        #region Methods
        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Username = HttpContext.Session.GetString("Username");
                if (Username == null)
                {
                    return RedirectToPage("/Users/Login");
                }
                else
                {
                    CurrentUser = await _userService.GetUserByUsernameAsync(Username);
                }
                
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
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
        /*public async Task<IActionResult> OnGetAsync()
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
        }*/

        public async Task<IActionResult> OnPostDeleteAsync(string username)
        {
            await _userService.DeleteUserAsync(username);
            return RedirectToPage("GetAllUsers");
        }
        #endregion
    }
}
