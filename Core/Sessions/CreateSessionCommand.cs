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


	public class CreateSessionCommand : Command<bool>
	{
		private ICreateSessionMessage message;

		public CreateSessionCommand(ICreateSessionMessage message)
		{
			this.message = message;
		}		

		public override bool Execute()
		{
			bool isAuthorized = false;
			
			var user = All<User>().FirstOrDefault(u => u.Username == message.Username);

			if (user != null)
				isAuthorized = user.HasPassword(message.Password);
			
			return isAuthorized;
		}
	}
}
