using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Users;

namespace Core.Libraries
{
    public static class LibraryExtensions
    {
        public static ILibrary ToViewModel(this Library library)
        {
            return new LibraryViewModel
            {
                Id = library.Id,
                Name = library.Name,
                Creator = library.Creator,
                Created = library.Created.ToDateString()
            };
        }

        private class LibraryViewModel : ILibrary
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public IUser Creator { get; set; }
            public string Created { get; set; }
        }
    }
}
