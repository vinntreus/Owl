using System.Collections.Generic;
using System.Linq;
using Core.Libraries;
using Core.Users;
using Raven.Client;

namespace Core.Queries
{
    public class LibraryQuery : IQuery<LibraryViewModel>
    {
        private int id;
        public LibraryQuery(int id)
        {
            this.id = id;
        }
        public LibraryViewModel Execute(IDocumentSession session)
        {
            var library = session.Load<Library>(id);      

            return new LibraryViewModel(library.ToViewModel());
        }      
    }


    public class LibraryViewModel
    {
        public LibraryViewModel(ILibrary library)
        {
            Library = library;
        }

        public ILibrary Library { get; private set; }
    }
}
