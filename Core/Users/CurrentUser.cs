using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;

namespace Core.Users
{
    public class CurrentUser
    {
        private IQueryable<User> users;
        public CurrentUser(IQueryable<User> users)
        {
            this.users = users;
        }
        public User Get()
        {
            var identity = System.Web.HttpContext.Current.User.Identity;
            if (!identity.IsAuthenticated)
                return null;

            var username = identity.Name;

            return users.First(u => u.Username == username);
        }
    }

    public static class DocumentSessionExtensions
    {
        public static User GetCurrentUser(this IDocumentSession session)
        {
            return new CurrentUser(session.Query<User>()).Get();
        }
    }
}
