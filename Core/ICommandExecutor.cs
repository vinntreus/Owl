using Raven.Client;

namespace Core
{
    public interface ICommandExecutor
    {
        void Execute(Command command);
		T Execute<T>(Command<T> command);
    }

    public class CommandExecutor : ICommandExecutor
    {
        private readonly IDocumentSession session;

        public CommandExecutor(IDocumentSession session)
        {
            this.session = session;
        }

        public void Execute(Command command)
        {
            command.Session = session;
            command.Execute();
        }


		public T Execute<T>(Command<T> command)
		{
			command.Session = session;
			return command.Execute();
		}
	}
}