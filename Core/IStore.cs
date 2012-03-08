using System.Collections.Generic;
using System.Linq;
using Core.Users;
using Raven.Client;

namespace Core
{
    public interface IStore
    {
        IEnumerable<IUser> AllUsers();
    }

    public class Store : IStore
    {
        private readonly IDocumentSession session;

        public Store(IDocumentSession session)
        {
            this.session = session;
        }

        public IEnumerable<IUser> AllUsers()
        {
            return session.Query<User>().ToList();           
        }
    }
}