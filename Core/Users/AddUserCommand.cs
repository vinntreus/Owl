using System.Linq;

namespace Core.Users
{
	public class AddUserCommand : Command
	{
		private readonly IAddUserMessage message;

		public AddUserCommand(IAddUserMessage message)
		{
			this.message = message;
		}

		public override void Execute()
        {
            var user = User.Create(message);

			InSession((s) =>
			{
				if (s.Query<User>().Any(u => u.Username == message.Username))
					throw new CreateUserException("User already exists");

				s.Store(user);
				s.SaveChanges();
			});
        }
	}
}
