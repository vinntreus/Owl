using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Libraries;

namespace Web.Models
{
    public class CreateLibraryViewModel : ICreateLibraryMessage
    {
        [Required(ErrorMessage = "Must enter a name")]
        public string Name { get; set; }
    }
}   