using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Users;

namespace Core.Books
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public User Creator { get; set; } 
    }
}
