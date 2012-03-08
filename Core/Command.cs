using System;
using System.Linq;
using Raven.Client;

namespace Core
{
	public abstract class CommandBase
	{
        public IDocumentSession Session { get; set; }

        public virtual IQueryable<T> All<T>()
        {
            return Session.Query<T>();
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