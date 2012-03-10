using System.Collections.Generic;
using System.Web.Mvc;
using Core;
using Core.Users;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models;
using System.Linq;
using Web.Security;

namespace UnitTests.Web.Controllers
{
    [TestFixture]
    public class UsersControllerTests
    {
        private Mock<IStore> storeMock; 
        private UsersController controller;
        private Mock<ICommandExecutor> commandMock;
        private Mock<IAuthenticator> authenticatorMock;

        [SetUp]
        public void Setup()
        {
            storeMock = new Mock<IStore>();
            commandMock = new Mock<ICommandExecutor>();
            authenticatorMock = new Mock<IAuthenticator>();
            
            commandMock.Setup(c => c.Execute(It.IsAny<CreateUserCommand>())).Returns(new CommandResult<IUser>(new User { Username = "a" }));
            controller = new UsersController(commandMock.Object, storeMock.Object, authenticatorMock.Object);
        }

        [Test]
        public void Index_Always_ReturnsAllUsers()
        {
            var expectedList = new List<IUser>();
            storeMock.Setup(u => u.Execute(It.IsAny<UsersQuery>())).Returns(expectedList);

            var actionResult = controller.Index();

            Assert.That(actionResult.Model, Is.SameAs(expectedList));
        }

        [Test]
        public void Create_Get_ReturnsCreateView()
        {
            var result = controller.Create();

            Assert.That(result.ViewName, Is.EqualTo(string.Empty));
        }

        [Test]
        public void Create_Get_AllowsHttpGet()
        {
            var hasAttribute = controller.HasAttribute("Create", typeof(HttpGetAttribute));

            Assert.That(hasAttribute, Is.True);
        }

        [Test]
        public void Create_Post_AllowsHttpPost()
        {
            var hasAttribute = controller.HasAttribute("Create", typeof(HttpPostAttribute), typeof(AddUserMessage));

            Assert.That(hasAttribute, Is.True);
        }

        [Test]
        public void Create_Post_RedirectsToIndexOfHomeController()
        {
            var result = (RedirectToRouteResult)controller.Create(new AddUserMessage {Username = "a", Password = "b"});

            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.EqualTo("Home"));
        }

        [Test]
        public void Create_PostWithInvalidModelState_ReturnsViewWithPassedMessage()
        {
            controller.ModelState.AddModelError("fel", "felet");
            var createUserMessage = new AddUserMessage();
            var result = (ViewResult)controller.Create(createUserMessage);

            Assert.That(result.ViewName, Is.EqualTo(""));
            Assert.That(result.Model, Is.SameAs(createUserMessage));
        }

        [Test]
        public void Create_PostAndModelStateIsValid_AddMessage()
        {
            var createUserMessage = new AddUserMessage();

            controller.Create(createUserMessage);

            commandMock.Verify(u => u.Execute(It.IsAny<CreateUserCommand>()));
        }

        [Test]
        public void Create_PostWhichMakeTheCommandFail_ReturnsErrorFromCommand()
        {
            commandMock.Setup(c => c.Execute(It.IsAny<CreateUserCommand>())).Returns(new CommandResult<IUser>("fel"));

            var result = (ViewResult)controller.Create(new AddUserMessage());

            Assert.That(result.ViewName, Is.EqualTo(""));
            Assert.That(controller.ModelState.Values.First().Errors[0].ErrorMessage, Is.EqualTo("fel"));
        }
    }
}
