using Core.Sessions;
using Raven.Client;

namespace Core.Activities
{
    public class CreatedSessionActivity : ActivityHandler<CreatedSession>
    {
        public CreatedSessionActivity(IDocumentSession session) : base(session) {}

        public override string GetActivityText(CreatedSession args)
        {
            return string.Format("{0} logged in", args.User.Username);
        }
    }  
}
