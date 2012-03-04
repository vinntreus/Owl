using System.Collections.Generic;
using System.Web.Mvc;
using Core;
using Core.Users;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models;

namespace UnitTests.Web.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IStore> storeMock; 
        private UsersController controller;
        private Mock<ICommandExecutor> commandMock;

        [SetUp]
        public void Setup()
        {
            storeMock = new Mock<IStore>();
            commandMock = new Mock<ICommandExecutor>();
            controller = new UsersController(commandMock.Object, storeMock.Object);
        }

        [Test]
        public void Index_ReturnsAllUsers()
        {
            var expectedList = new List<IUser>();
            storeMock.Setup(u => u.AllUsers()).Returns(expectedList);

            var actionResult = controller.Index();

            Assert.That(actionResult.Model, Is.SameAs(expectedList));
        }

        [Test]
        public void CreateGet_ReturnsCreateView()
        {
            var result = controller.Create();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void CreateGet_AllowsHttpGet()
        {
            var hasAttribute = controller.HasAttribute("Create", typeof(HttpGetAttribute));

            Assert.That(hasAttribute, Is.True);
        }

        [Test]
        public void CreatePost_AllowsHttpPost()
        {
            var hasAttribute = controller.HasAttribute("Create", typeof(HttpPostAttribute), typeof(AddUserMessage));

            Assert.That(hasAttribute, Is.True);
        }

        [Test]
        public void CreatePost_RedirectsToIndex()
        {
            var result = (RedirectToRouteResult)controller.Create(new AddUserMessage {Username = "a", Password = "b"});

            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.Null);
        }

        [Test]
        public void CreatePost_ModelStateIsInvalid_ReturnsViewWithPassedMessage()
        {
            controller.ModelState.AddModelError("fel", "felet");
            var createUserMessage = new AddUserMessage();
            var result = (ViewResult)controller.Create(createUserMessage);

            Assert.That(result.ViewName, Is.EqualTo(""));
            Assert.That(result.Model, Is.SameAs(createUserMessage));
        }

        [Test]
        public void CreatePost_ModelStateIsValid_AddMessage()
        {
            var createUserMessage = new AddUserMessage();

            controller.Create(createUserMessage);

            commandMock.Verify(u => u.Execute(It.IsAny<AddUserCommand>()));
        }
    }
}
