using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;


namespace Core.Users
{
    public class UsersQuery : IQuery<IEnumerable<IUser>>
    {
        public IEnumerable<IUser> Execute(IDocumentSession session)
        {
            return session.Query<User>().ToList();
        }
    }
}
