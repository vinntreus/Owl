using System.ComponentModel.DataAnnotations;
using Core;
using Core.Users;

namespace Web.Models
{
    public class AddUserMessageMessage : IAddUserMessage
    {
        [Required(ErrorMessage = "Must enter a username")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Must enter a password")]
        public string Password { get; set; }
        
    }
}