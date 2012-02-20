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
        private readonly IDocumentStore store;

        public Store(IDocumentStore store)
        {
            this.store = store;
        }

        public IEnumerable<IUser> AllUsers()
        {
            using (var session = store.OpenSession())
            {
                return session.Query<User>().ToList();
            }
        }
    }
}