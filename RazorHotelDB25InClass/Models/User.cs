using System.ComponentModel.DataAnnotations;

namespace RazorHotelDB25InClass.Models
{
    public class User
    {
        [Required(ErrorMessage = "Username is required")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public User()
        {

        }

        public User(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}
