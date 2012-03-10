using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Users;

namespace Core.Libraries
{
    public class Library 
    {
        public int Id { get; set; }
        public User Creator { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }

        internal static Library Create(ICreateLibraryMessage message)
        {
            return new Library
            {
                Created = DateTime.Now,
                Name = message.Name
            };
        }
    }   
}
