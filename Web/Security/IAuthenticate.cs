using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Security;

namespace Web.Security
{
	public interface IAuthenticator
	{
		void SetAuthCookie(string userName, bool createPersistentCookie);
        void SignOut();
	}  

	public class FormsAuthenticator : IAuthenticator
	{
		public void SetAuthCookie(string userName, bool createPersistentCookie)
		{
			FormsAuthentication.SetAuthCookie(userName, createPersistentCookie);            
		}

        public void SignOut()
        {
          FormsAuthentication.SignOut();
        }
    }
}
