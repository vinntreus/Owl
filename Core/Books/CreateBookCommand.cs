using System;
using Core.Libraries;
using Core.Users;

namespace Core.Books
{
    public interface ICreateBookMessage
    {
        int LibraryId { get; }

        string Title { get; }
        string Description { get; }
        string Author { get; }
        DateTime? Published { get; }
        string Tags { get; }
        string CoverSource { get; }
    }

    public class CreatedBook : IDomainEvent
    {
        public CreatedBook(Book book, Library inLibrary)
        {
            if (book == null)
                throw new ArgumentNullException("book");

            this.Book = book;
            this.Library = inLibrary;
        }

        public Book Book { get; private set; }
        public Library Library { get; private set; }
    }    

    public class CreateBookCommand : Command<IBook>
    {
        private ICreateBookMessage message;

        public CreateBookCommand(ICreateBookMessage message)
        {
            this.message = message;
        }
        public override CommandResult<IBook> Execute()
        {
            var book = Book.Create(message);
            book.Creator = this.CurrentUser;

            var library = Session.Load<Library>(message.LibraryId);

            if(library != null)
                library.AddBook(book);
                        
            Session.Store(book);
            Session.SaveChanges();

            DomainEvents.Raise(new CreatedBook(book, library));

            return new CommandResult<IBook>(book);
        }
    }
}
