using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Activities
{
    public class ActivityCommand : Command<Activity>
    {
        private string activity;

        public ActivityCommand(string activity)
        {
            this.activity = activity;
        }

        public override Activity Execute()
        {
            var a = Activity.Create(activity);
            Session.Store(a);
            Session.SaveChanges();
            return a;
        }      
    }
}
