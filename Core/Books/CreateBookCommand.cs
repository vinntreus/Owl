using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Books
{
    public interface CreateBookMessage
    {
        string Title { get; }
    }

    public class CreateBookCommand : Command<IBook>
    {
        private CreateBookMessage message;

        public CreateBookCommand(CreateBookMessage message)
        {
            this.message = message;
        }
        public override CommandResult<IBook> Execute()
        {
            throw new NotImplementedException();
        }
    }
}
