using Core.Activities;
using Raven.Client;

namespace Core
{
    public interface ICommandExecutor
    {
		CommandResult<T> Execute<T>(Command<T> command);
    }

    public class CommandExecutor : ICommandExecutor
    {
        private readonly IDocumentSession session;

        public CommandExecutor(IDocumentSession session)
        {
            this.session = session;
        }

		public CommandResult<T> Execute<T>(Command<T> command)
		{
			command.Session = session;
           
			return command.Execute();
		}
	}
}