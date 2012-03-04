using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Core;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models;

namespace UnitTests.Web.Controllers
{
	[TestFixture]
	public class SessionControllerTests
	{
		private SessionController controller;
		private Mock<ICommandExecutor> commandMock;

		[SetUp]
		public void Setup()
		{
			commandMock = new Mock<ICommandExecutor>();
			controller = new SessionController(commandMock.Object);
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

		[Test]
		public void CreatePost_AllowsHttpPost()
		{
			var hasAttribute = controller.HasAttribute("Create", typeof(HttpPostAttribute), typeof(SessionViewModel));

			Assert.That(hasAttribute, Is.True);
		}

		[Test]
		public void CreatePost_ModelStateIsInvalid_ReturnsViewWithPassedMessage()
		{
			controller.ModelState.AddModelError("fel", "felet");
			var viewModel = new SessionViewModel();
			var result = (ViewResult)controller.Create(viewModel);

			Assert.That(result.ViewName, Is.EqualTo(""));
			Assert.That(result.Model, Is.SameAs(viewModel));
		}
		

	}
}
