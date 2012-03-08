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

		public virtual IQueryable<T> All<T>(IDocumentSession session)
		{
			return session.Query<T>();
		}

		public override bool Execute()
		{
			Console.WriteLine("hippi");
			bool isAuthorized = false;
			InSession((s) =>
			{
				var user = All<User>(s).FirstOrDefault(u => u.Username == message.Username);

				if (user != null)
					isAuthorized = user.HasPassword(message.Password);
			});
			return isAuthorized;
		}
	}
}
