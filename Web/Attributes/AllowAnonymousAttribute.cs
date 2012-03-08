using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Attributes
{
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public class AllowAnonymousAttribute : Attribute
	{
	}	
}