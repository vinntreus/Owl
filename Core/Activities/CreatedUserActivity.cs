using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Users;
using Raven.Client;

namespace Core.Activities
{
    public class CreatedUserActivity : ActivityHandler<CreatedUser>
    {
        public CreatedUserActivity(IDocumentSession session) : base(session) { }

        public override string GetActivityText(CreatedUser args)
        {
            return string.Format("{0} was created", args.User.Username);
        }
    }
}
