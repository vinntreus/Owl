using System;
using System.Linq;
using Core.Users;
using Raven.Client;

namespace Core
{
	public abstract class Command<T>
	{
		public abstract CommandResult<T> Execute();

        public IDocumentSession Session { get; set; }

        public virtual IQueryable<T> All<T>()
        {
            return Session.Query<T>();
        }

        public virtual User CurrentUser
        {
            get
            {
                var identity = System.Web.HttpContext.Current.User.Identity;
                if (!identity.IsAuthenticated)
                    return null;

                var username = identity.Name;

                return All<User>().First(u => u.Username == username);
            }
        }
	}   
}