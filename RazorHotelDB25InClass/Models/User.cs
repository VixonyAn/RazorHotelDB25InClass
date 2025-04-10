using System.ComponentModel.DataAnnotations;

namespace RazorHotelDB25InClass.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username is required")]
        [StringLength(30, ErrorMessage = "Exceeded character length of 30")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Exceeded character length of 50")]
        public string Password { get; set; }

        [StringLength(100, ErrorMessage = "Exceeded character length of 100")]
        public string? ImageUrl { get; set; }

        public User() { }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public User(string username, string password, string imageUrl)
        {
            Username = username;
            Password = password;
            ImageUrl = imageUrl;
        }
    }
}
