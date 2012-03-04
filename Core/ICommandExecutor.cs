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
        private readonly IDocumentStore store;

        public CommandExecutor(IDocumentStore store)
        {
            this.store = store;
        }

        public void Execute(Command command)
        {
            command.Store = store;
            command.Execute();
        }


		public T Execute<T>(Command<T> command)
		{
			command.Store = store;
			return command.Execute();
		}
	}
}