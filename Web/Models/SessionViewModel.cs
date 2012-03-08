using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Core.Sessions;

namespace Web.Models
{
	public class SessionViewModel : ICreateSessionMessage
	{
		[Required(ErrorMessage = "Must enter a username")]
		[Display(Name = "Username")]
		public string Username { get; set; }

		[Required(ErrorMessage = "Must enter a password")]
		public string Password { get; set; }

		public bool PersistCookie { get; set; }
	}
}