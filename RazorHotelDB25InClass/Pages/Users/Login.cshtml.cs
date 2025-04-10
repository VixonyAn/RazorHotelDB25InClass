using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Models;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Services;

namespace RazorHotelDB25InClass.Pages.Users
{
    public class LoginModel : PageModel
    {
        #region Instance Fields
        private IUserService _userService;
        #endregion

        #region Properties
        [BindProperty] public User User { get; set; }
        public string MessageError { get; set; }
        #endregion

        #region Constructors
        public LoginModel(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region Methods
        public async Task OnGetAsync() { }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("Username");
            return RedirectToPage("Login");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) { return Page(); }
            try
            {
                User? loginUser = await _userService.VerifyUserAsync(User.Username, User.Password);
                if (loginUser != null)
                {
                    HttpContext.Session.SetString("Username", loginUser.Username);
                    return RedirectToPage("GetAllUsers");
                }
                else
                { // if user cannot be verified
                    MessageError = "Invalid username or password";
                    return Page();
                }
            }
            catch (Exception ex)
            { // catches exceptions like weak connection or mistake in query
                ViewData["ErrorMessage"] = ex.Message;
                return Page();
            }
        }
        #endregion
    }
}
