using Raven.Client;

namespace Core
{
    public interface ICommandExecuter
    {
        void ExecuteCommand(Command command);
    }

    public class CommandExecuter : ICommandExecuter
    {
        private readonly IDocumentStore store;

        public CommandExecuter(IDocumentStore store)
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