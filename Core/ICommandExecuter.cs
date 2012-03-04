using Raven.Client;

namespace Core
{
    public interface ICommandExecutor
    {
        void ExecuteCommand(Command command);
    }

    public class CommandExecutor : ICommandExecutor
    {
        private readonly IDocumentStore store;

        public CommandExecutor(IDocumentStore store)
        {
            this.store = store;
        }

        public void ExecuteCommand(Command command)
        {
            command.Store = store;
            command.Execute();
        }
    }
}