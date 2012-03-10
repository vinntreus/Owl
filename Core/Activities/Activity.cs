using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Activities
{
    
    public class Activity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }

        public static Activity Create(string text)
        {
            return new Activity
            {
                Date = DateTime.Now,
                Text = text
            };
        }       
    }   
}
