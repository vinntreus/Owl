using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Users;
using Raven.Client;

namespace Core.Sessions
{
	public interface ICreateSessionMessage
	{
		string Username { get; }
		string Password { get; }
	}


	public class CreateSessionCommand : Command<CommandResult<IUser>>
	{
		private ICreateSessionMessage message;

		public CreateSessionCommand(ICreateSessionMessage message)
		{
			this.message = message;
		}

        public override CommandResult<IUser> Execute()
		{
			var user = All<User>().FirstOrDefault(u => u.Username == message.Username);
            var result = new CommandResult<IUser>(user);
            if (user == null)
            {
                result.AddError("Username does not exist");
            }
            else if(!user.HasPassword(message.Password))
            {
                result.AddError("Wrong password");
            }               
			
			return result;
		}     
    }
}
