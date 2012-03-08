using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Attributes
{
	public class RequireAuthorizationAttribute : AuthorizeAttribute
	{
		public override void OnAuthorization(AuthorizationContext filterContext)
		{
			var skipAuthorization = filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true) ||
									filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(
										typeof(AllowAnonymousAttribute), true);
			if (!skipAuthorization)
			{
				base.OnAuthorization(filterContext);
			}
		}
	}
}