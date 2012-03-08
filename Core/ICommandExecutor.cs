using Raven.Client;

namespace Core
{
    public interface ICommandExecutor
    {
		T Execute<T>(Command<T> command);
    }

    public class CommandExecutor : ICommandExecutor
    {
        private readonly IDocumentSession session;

        public CommandExecutor(IDocumentSession session)
        {
            this.session = session;
        }

		public T Execute<T>(Command<T> command)
		{
			command.Session = session;
			return command.Execute();
		}
	}
}