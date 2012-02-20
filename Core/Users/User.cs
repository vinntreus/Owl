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
                Password = message.Password
            };
        }
    }
}