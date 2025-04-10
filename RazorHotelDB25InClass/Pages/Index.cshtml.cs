using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IUserService _userService;

        public string Username { get; set; }
        public User CurrentUser { get; set; }

        public IndexModel(ILogger<IndexModel> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            try
            {
                Username = HttpContext.Session.GetString("Username");
                if (Username == null)
                {
                    return RedirectToPage("Users/Login");
                }
                else
                {
                    CurrentUser = await _userService.GetUserByUsernameAsync(Username);
                }
                return Page();
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }
    }
}
