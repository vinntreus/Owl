using DevOne.Security.Cryptography.BCrypt;

namespace Core.Users
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static User Create(ICreateUserMessage message)
        {
            return new User
            {
                Username = message.Username,
				Password = HashPassword(message.Password)
            };
        }

		private static string HashPassword(string password)
		{			
			return BCryptHelper.HashPassword(password, BCryptHelper.GenerateSalt());
		}

		public bool HasPassword(string p)
		{
            return BCryptHelper.CheckPassword(p, Password);
		}
	}
}