using System;
using Raven.Client;

namespace Core
{
	public abstract class CommandBase
	{
		public IDocumentStore Store { get; set; }

		protected void InSession(Action<IDocumentSession> action)
		{
			using (var session = Store.OpenSession())
			{
				action(session);
			}
		}
	}

    public abstract class Command : CommandBase
    {      
        public abstract void Execute();		
	}

	public abstract class Command<T> : CommandBase
	{
		public abstract T Execute();		
	}
}