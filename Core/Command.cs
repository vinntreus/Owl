using Raven.Client;

namespace Core
{
    public abstract class Command
    {
        public IDocumentStore Store { get; set; }

        public abstract void Execute();
    }
}