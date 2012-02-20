using Core;
using Core.Users;

namespace Web.Models
{
    public class UserViewModel : IUser
    {
        public string Username { get; set; }
    }
}