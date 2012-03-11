using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Books;
using Core.Libraries;
using Raven.Client;

namespace Core.Queries
{
    public class BookQuery : IQuery<BookViewModel>
    {
        private int id;
        private int libraryId;
        public BookQuery(int id, int libraryId)
        {
            this.id = id;
            this.libraryId = libraryId;
        }
        public BookViewModel Execute(IDocumentSession session)
        {
            var book = session.Load<Book>(id);
            var library = session.Load<Library>(libraryId);

            return new BookViewModel(book, library.ToViewModel());
        }
    }


    public class BookViewModel
    {
        public BookViewModel(IBook book, ILibrary library)
        {
            this.Book = book;
            this.Library = library;
        }

        public ILibrary Library { get; private set; }
        public IBook Book { get; private set; }
    }
}
