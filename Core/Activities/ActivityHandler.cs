using Raven.Client;

namespace Core.Activities
{
    public abstract class ActivityHandler<T> : IHandle<T> where T : IDomainEvent
    {
        private IDocumentSession session;

        public ActivityHandler(IDocumentSession session)
        {
            this.session = session;
        }

        public abstract string GetActivityText(T args);

        public void Handle(T args)
        {
            var activity = Activity.Create(GetActivityText(args));
            session.Store(activity);
            session.SaveChanges();
        }
    }
}
