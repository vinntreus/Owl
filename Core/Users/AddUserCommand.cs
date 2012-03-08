using System.Collections.Generic;
using System.Linq;

namespace Core.Users
{
	public class AddUserCommand : Command<CommandResult<IUser>>
	{
		private readonly IAddUserMessage message;

		public AddUserCommand(IAddUserMessage message)
		{
			this.message = message;
		}

        public override CommandResult<IUser> Execute()
        {
            if (All<User>().Any(u => u.Username == message.Username))
                return new CommandResult<IUser>("User already exists");
            
            var user = User.Create(message);

            Session.Store(user);
            Session.SaveChanges();

            return new CommandResult<IUser>(user);
        }     
    }   
}
