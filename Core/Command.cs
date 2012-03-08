using System;
using System.Linq;
using Raven.Client;

namespace Core
{
	public abstract class Command<T>
	{
		public abstract T Execute();

        public IDocumentSession Session { get; set; }

        public virtual IQueryable<T> All<T>()
        {
            return Session.Query<T>();
        }       
	}   
}