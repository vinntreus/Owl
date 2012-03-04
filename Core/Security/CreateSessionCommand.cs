using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Security
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
			return true;
		}
	}
}
