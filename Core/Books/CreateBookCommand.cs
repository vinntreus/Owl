using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Books
{
    public interface ICreateBookMessage
    {
        string Title { get; }
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
            throw new NotImplementedException();
        }
    }
}
