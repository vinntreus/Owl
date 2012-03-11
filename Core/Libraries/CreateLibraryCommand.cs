using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Books;
using Core.Users;

namespace Core.Libraries
{
    public interface ILibrary
    {
        int Id { get; }
        string Name { get; }
        IUser Creator { get; }
        string Created { get; }
        IEnumerable<IBook> Books { get; }
    }

    public interface ICreateLibraryMessage
    {
        string Name { get; }
    }

    public class CreatedLibrary : IDomainEvent
    {
        public CreatedLibrary(Library library)
        {
            if (library == null)
                throw new ArgumentNullException("library");

            this.Library = library;
        }

        public Library Library { get; private set; }
    }    

    public class CreateLibraryCommand : Command<ILibrary>
    {
        private ICreateLibraryMessage message;

        public CreateLibraryCommand(ICreateLibraryMessage message)
        {
            this.message = message;
        }

        public override CommandResult<ILibrary> Execute()
        {
            var library = Library.Create(message);
            library.Creator = CurrentUser;

            Session.Store(library);
            Session.SaveChanges();

            DomainEvents.Raise(new CreatedLibrary(library));

            return new CommandResult<ILibrary>(library.ToViewModel());
        }

       
    }
}
