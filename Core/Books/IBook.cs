using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Books
{
    public interface IBook
    {
        int Id { get; }
        string Title { get; }
    }
}
