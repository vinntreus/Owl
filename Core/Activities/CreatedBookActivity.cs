using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Books;
using Raven.Client;

namespace Core.Activities
{
    public class CreatedBookActivity: ActivityHandler<CreatedBook>
    {
        public CreatedBookActivity(IDocumentSession session) : base(session) { }

        public override string GetActivityText(CreatedBook args)
        {
            return string.Format("{0} created the book '{1}' {2}", args.Book.Creator.Username,
                                                                   args.Book.Title,
                                                                   args.Library != null ? "in '" + args.Library.Name + "'" : "");
        }
    }  
}
