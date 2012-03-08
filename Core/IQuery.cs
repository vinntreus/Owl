using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;


namespace Core
{
    public interface IQuery<T>
    {
        T Execute(IDocumentSession session);
    }
}
