using Raven.Client;
using Raven.Client.Document;

namespace Web
{
    public static class Store
    {
        private static IDocumentStore s;

        public static IDocumentStore DocumentStore
        {
            get
            {
                if (s == null)
                {
                    s = new DocumentStore { ConnectionStringName = "RavenDB" };
                    s.Initialize();
                }
                return s;
            }
        }

    }
}