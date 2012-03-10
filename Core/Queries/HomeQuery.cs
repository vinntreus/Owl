using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Activities;

namespace Core.Queries
{
    public class HomeQuery : IQuery<HomeViewModel>
    {

        public HomeViewModel Execute(Raven.Client.IDocumentSession session)
        {
            var activities = session.Query<Activity>().OrderByDescending(a => a.Date).ToList().Select(a => new ActivityViewModel(a.Date.ToDateString(), a.Text));

            return new HomeViewModel(activities);
        }
    }
    
    public class ActivityViewModel
    {
        public ActivityViewModel(string date, string text)
        {
            Date = date;
            Text = text;
        }

        public string Date { get; private set; }
        public string Text { get; private set; }
    }

    public class HomeViewModel
    {
        public HomeViewModel(IEnumerable<ActivityViewModel> activities)
        {
            Activities = activities;
        }

        public IEnumerable<ActivityViewModel> Activities { get; private set; }
    }
}
