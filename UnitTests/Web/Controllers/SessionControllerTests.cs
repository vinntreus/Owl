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
using Web.Security;

namespace UnitTests.Web.Controllers
{
	[TestFixture]
	public class SessionControllerTests
	{
		private SessionController controller;
		private Mock<ICommandExecutor> commandMock;
		private Mock<IAuthenticator> authenticatorMock;

		[SetUp]
		public void Setup()
		{
			commandMock = new Mock<ICommandExecutor>();
			authenticatorMock = new Mock<IAuthenticator>();
			controller = new SessionController(commandMock.Object, authenticatorMock.Object);
		}

		[Test]
		public void Create_Get_ShouldReturnView()
		{
			var result = (ViewResult)controller.Create();

			Assert.That(result.ViewName, Is.EqualTo(""));
		}

		[Test]
		public void Create_Get_AllowsHttpGet()
		{
			var result = controller.HasAttribute("Create", typeof(HttpGetAttribute));

			Assert.That(result, Is.True);
		}

		[Test]
		public void Create_Post_AllowsHttpPost()
		{
			var hasAttribute = controller.HasAttribute("Create", typeof(HttpPostAttribute), typeof(SessionViewModel));

			Assert.That(hasAttribute, Is.True);
		}

		[Test]
		public void Create_PostsModelStateIsInvalid_ReturnsViewWithPassedMessage()
		{
			controller.ModelState.AddModelError("fel", "felet");
			var viewModel = new SessionViewModel();
			var result = (ViewResult)controller.Create(viewModel);

			Assert.That(result.ViewName, Is.EqualTo(""));
			Assert.That(result.Model, Is.SameAs(viewModel));
		}

		[Test]
		public void Create_PostsInvalidCredential_ReturnToSameViewWithModelError()
		{
			commandMock.Setup(c => c.Execute<bool>(It.IsAny<Command<bool>>())).Returns(false);

			var result = (ViewResult)controller.Create(new SessionViewModel());

			Assert.That(result.ViewName, Is.EqualTo(""));
			Assert.That(controller.ModelState.First().Value.Errors[0].ErrorMessage, Is.EqualTo("Wrong username or password"));
		}


		[Test]
		public void Create_PostsValidCredential_RedirectToHome()
		{
			commandMock.Setup(c => c.Execute<bool>(It.IsAny<Command<bool>>())).Returns(true);

			var result = (RedirectToRouteResult)controller.Create(new SessionViewModel());

			Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
			Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
		}

		[Test]
		public void Create_PostsValidCredential_SetsAuthCookie()
		{
			commandMock.Setup(c => c.Execute<bool>(It.IsAny<Command<bool>>())).Returns(true);

			var result = (RedirectToRouteResult)controller.Create(new SessionViewModel{ Username = "a", PersistCookie = true });

			authenticatorMock.Verify(a => a.SetAuthCookie("a", true));
		}

        [Test]
        public void Destroy_Always_CallsSignOut()
        {
            controller.Destroy();

            authenticatorMock.Verify(a => a.SignOut());
        }

        [Test]
        public void Destroy_Always_RedirectsToHome()
        {
            var result = controller.Destroy();

            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
        }
	}
}
