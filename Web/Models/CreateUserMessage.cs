using System.ComponentModel.DataAnnotations;
using Core;

namespace Web.Models
{
    public class CreateUserMessage : ICreateUser
    {
        [Required(ErrorMessage = "Must enter a username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Must enter a password")]
        public string Password { get; set; }
        
    }
}