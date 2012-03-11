using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class CreateBookViewModel
    {
        public int LibraryId { get; set; }

        [Required(ErrorMessage="Must enter a title")]
        public string Title { get; set; }

        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime? Published { get; set; }
        [Display(Name="Tags (commaseparated)")]
        public string Tags { get; set; }
        public string CoverSource { get; set; }
    }
}