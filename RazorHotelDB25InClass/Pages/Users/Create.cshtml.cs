using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorHotelDB25InClass.Interfaces;
using RazorHotelDB25InClass.Models;

namespace RazorHotelDB25InClass.Pages.Users
{
    public class CreateModel : PageModel
    {
        #region Instance Fields
        private IUserService _userService;
        private IWebHostEnvironment _webHostEnvironment;
        #endregion

        #region Properties
        [BindProperty] public User User { get; set; }
        [BindProperty] public IFormFile? Photo { get; set; }
        #endregion

        #region Constructor
        public CreateModel(IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }
        #endregion

        #region Methods
        public void OnGet() { }
        public async Task<IActionResult> OnPostAsync()
        {
            // if no file is selected, set to default
            if (Photo == null)
            {
                User.ImageUrl = "defaultImage.jpg";
            }
            // upload photo
            if (Photo != null)
            {
                if (User.ImageUrl != null)
                {
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "/images/memberimages", User.ImageUrl);
                    System.IO.File.Delete(filePath);
                }
                User.ImageUrl = ProcessUploadedFile();
            }

            // if ModelState is NOT valid, reload (triggers error messages)
            if (!ModelState.IsValid) { return Page(); }            

            try
            {
                await _userService.CreateUserAsync(User);
                return RedirectToPage("GetAllUsers");
            }
            catch (Exception ex)
            {
                ViewData["ErrorMessage"] = ex.Message;
            }
            return Page();
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images/memberimages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        #endregion
    }
}
