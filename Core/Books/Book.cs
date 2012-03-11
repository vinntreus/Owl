using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Users;

namespace Core.Books
{
    public class Book : IBook
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User Creator { get; set; }

        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Published { get; set; }
        public string[] Tags { get; set; }
        public string CoverSource { get; set; }

        internal static Book Create(ICreateBookMessage message)
        {
            var book = new Book
            {
                Title = message.Title,
                Description = message.Description,
                Author = message.Author,
                Published = message.Published != null ? message.Published.Value : DateTime.Now,                
                CoverSource = message.CoverSource
            };

            if(!string.IsNullOrEmpty(message.Tags))
                book.Tags = message.Tags.Split(new []{','}, StringSplitOptions.RemoveEmptyEntries);

            return book;
        }


        public string GetPublishedDate()
        {
            return Published.ToDateString();
        }
    }
}
