using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Libraries
{
    public interface ILibrary
    {
        int Id { get; }
        string Name { get; }
    }

    public interface ICreateLibraryMessage
    {
        string Name { get; }
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
            throw new NotImplementedException();
        }
    }
}
