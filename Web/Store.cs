using Raven.Abstractions.Data;
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
                    var parser = ConnectionStringParser<RavenConnectionStringOptions>.FromConnectionStringName("RavenDB");
                    parser.Parse();

                    s = new DocumentStore
                    {
                        ApiKey = parser.ConnectionStringOptions.ApiKey,
                        Url = parser.ConnectionStringOptions.Url,
                    };
                    //s = new DocumentStore { ConnectionStringName = "RavenDB" };
                    s.Initialize();
                }
                return s;
            }
        }

    }
}