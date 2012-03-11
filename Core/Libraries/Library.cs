using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Books;
using Core.Users;

namespace Core.Libraries
{
    public class Library 
    {
        public Library()
        {
            Books = new List<Book>();
        }

        public int Id { get; set; }
        public User Creator { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public IList<Book> Books { get; set; }

        internal static Library Create(ICreateLibraryMessage message)
        {
            return new Library
            {
                Created = DateTime.Now,
                Name = message.Name
            };
        }

        internal void AddBook(Book book)
        {
            Books.Add(book);
        }
    }   
}
