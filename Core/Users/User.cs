using DevOne.Security.Cryptography.BCrypt;

namespace Core.Users
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static User Create(IAddUserMessage message)
        {
            return new User
            {
                Username = message.Username,
                Password = BCryptHelper.HashPassword(message.Password, BCryptHelper.GenerateSalt(12))
            };
        }
    }
}