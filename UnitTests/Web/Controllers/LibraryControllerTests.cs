using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Core;
using Core.Libraries;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models;

namespace UnitTests.Web.Controllers
{
    [TestFixture]
    public class LibraryControllerTests
    {
        private LibraryController controller;
        private Mock<ICommandExecutor> commandMock;

        [SetUp]
        public void Setup()
        {
            commandMock = new Mock<ICommandExecutor>();
            controller = new LibraryController(commandMock.Object);
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
            var hasAttribute = controller.HasAttribute("Create", typeof(HttpPostAttribute), typeof(CreateLibraryViewModel));

            Assert.That(hasAttribute, Is.True);
        }

        [Test]
        public void Create_PostsModelStateIsInvalid_ReturnsViewWithPassedMessage()
        {
            controller.ModelState.AddModelError("fel", "felet");
            var viewModel = new CreateLibraryViewModel();
            var result = (ViewResult)controller.Create(viewModel);

            Assert.That(result.ViewName, Is.EqualTo(""));
            Assert.That(result.Model, Is.SameAs(viewModel));
        }

        [Test]
        public void Create_OnCreated_RedirectToCreatedLibraryViewPage()
        {
            var library = new Mock<ILibrary>();
            library.Setup(l => l.Id).Returns(1);
            commandMock.Setup(c => c.Execute(It.IsAny<CreateLibraryCommand>())).Returns(new CommandResult<ILibrary>(library.Object));

            var result = (RedirectToRouteResult)controller.Create(new CreateLibraryViewModel());

            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.Null);
            Assert.That(result.RouteValues["id"], Is.EqualTo(1));
        }

        [Test]
        public void Create_OnFailedToCreate_ReturnsViewWithErrors()
        {
            commandMock.Setup(c => c.Execute(It.IsAny<CreateLibraryCommand>())).Returns(new CommandResult<ILibrary>("fel"));

            var result = (ViewResult)controller.Create(new CreateLibraryViewModel());

            Assert.That(result.ViewName, Is.EqualTo(""));
            Assert.That(controller.ModelState.First().Value.Errors[0].ErrorMessage, Is.EqualTo("fel"));
        }
    }
}
