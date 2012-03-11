using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Activities;
using Core.Libraries;
using Core.Users;

namespace Core.Queries
{
    public class HomeQuery : IQuery<HomeViewModel>
    {

        public HomeViewModel Execute(Raven.Client.IDocumentSession session)
        {
            var currentUser = session.GetCurrentUser();
            
            var libraries = session.Query<Library>()
                                    .Where(l => l.Creator.Id == currentUser.Id)
                                    .OrderByDescending(l => l.Created)
                                    .ToList()
                                    .Select(l => l.ToViewModel());

            var activities = session.Query<Activity>()
                                    .OrderByDescending(a => a.Date)
                                    .Take(10)
                                    .ToList()
                                    .Select(a => new ActivityViewModel(a.Date.ToDateString(), a.Text));            

            return new HomeViewModel(activities, libraries);
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
        public HomeViewModel(IEnumerable<ActivityViewModel> activities, IEnumerable<ILibrary> libraries)
        {
            Activities = activities;
            Libraries = libraries;
        }

        public IEnumerable<ActivityViewModel> Activities { get; private set; }

        public IEnumerable<ILibrary> Libraries { get; private set; }
    }
}
