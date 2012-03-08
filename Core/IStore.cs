using System.Collections.Generic;
using System.Linq;
using Core.Users;
using Raven.Client;

namespace Core
{
    public interface IStore
    {
        T Execute<T>(IQuery<T> query); 
    }

    public class Store : IStore
    {
        private readonly IDocumentSession session;

        public Store(IDocumentSession session)
        {
            this.session = session;
        }     

        public T Execute<T>(IQuery<T> query)
        {
            return query.Execute(session);
        }
    }
}