using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Libraries;
using Raven.Client;

namespace Core.Activities
{
    public class CreatedLibraryActivity : ActivityHandler<CreatedLibrary>
    {
        public CreatedLibraryActivity(IDocumentSession session) : base(session) { }
        
        public override string GetActivityText(CreatedLibrary args)
        {
            return string.Format("{0} created library: {1}", args.Library.Creator.Username, args.Library.Name);
        }
    }
}
