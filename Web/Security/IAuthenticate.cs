using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Security;

namespace Web.Security
{
	public interface IAuthenticator
	{
		void SetAuthCookie(string userName, bool createPersistentCookie);
	}

	public class FormsAuthenticator : IAuthenticator
	{
		public void SetAuthCookie(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);
		}
	}
}
