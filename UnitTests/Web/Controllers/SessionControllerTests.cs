using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using Web.Controllers;

namespace UnitTests.Web.Controllers
{
	[TestFixture]
	public class SessionControllerTests
	{
		private SessionController controller;

		[SetUp]
		public void Setup()
		{
			controller = new SessionController();
		}

		[Test]
		public void CreateGet_ShouldReturnView()
		{
			var result = (ViewResult)controller.Create();

			Assert.That(result.ViewName, Is.EqualTo(""));
		}

		[Test]
		public void CreateGet_AllowsHttpGet()
		{
			var result = controller.HasAttribute("Create", typeof(HttpGetAttribute));

			Assert.That(result, Is.True);
		}
		

	}
}
