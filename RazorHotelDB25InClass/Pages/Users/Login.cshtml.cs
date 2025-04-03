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
        public List<User> Users { get; set; }

        #endregion

        #region Constructors
        public LoginModel(IUserService userService)
        {
            _userService = userService;
            Users = _userService.GetAllUsersAsync().Result;
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
        { //nothing saved in loginUser
            User? loginUser = await _userService.VerifyUserAsync(User.Username, User.Password);
            if (loginUser != null)
            {
                HttpContext.Session.SetString("Username", loginUser.Username);
                return RedirectToPage("../Index");
            }
            else
            {
                MessageError = "Invalid username or password";
                return Page();
            }
            /*
            if (_userService.VerifyUserAsync(User.Username, User.Password) == null)
            {
                MessageError = "Invalid username or password";
                Password = ""; return Page();
            }
            User? loginUser = await _userService.VerifyUserAsync(User.Username, User.Password);
            HttpContext.Session.SetString("Username", loginUser.Username);
            return RedirectToPage("../Hotels/GetAllHotels"); 
            */
        }
        #endregion
    }
}
