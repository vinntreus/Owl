using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Core;
using Core.Books;
using Moq;
using NUnit.Framework;
using Web.Controllers;
using Web.Models;

namespace UnitTests.Web.Controllers
{
    [TestFixture]
    public class BookControllerTests
    {
        private BookController controller;
        private Mock<ICommandExecutor> commandMock;

        [SetUp]
        public void Setup()
        {
            commandMock = new Mock<ICommandExecutor>();
            controller = new BookController(commandMock.Object);
        }

        [Test]
        public void Create_Get_ShouldReturnView()
        {
            var result = (ViewResult)controller.Create(1);

            Assert.That(result.ViewName, Is.EqualTo(""));
        }

        [Test]
        public void Create_Get_ModelShouldHaveLibraryId()
        {
            var result = (ViewResult)controller.Create(1);
            var model = (CreateBookViewModel)result.Model;

            Assert.That(model.LibraryId, Is.EqualTo(1));
        }

        [Test]
        public void Create_Get_AllowsHttpGet()
        {
            var result = controller.HasAttribute("Create", typeof(HttpGetAttribute), typeof(int));

            Assert.That(result, Is.True);
        }

        [Test]
        public void Create_Post_AllowsHttpPost()
        {
            var hasAttribute = controller.HasAttribute("Create", typeof(HttpPostAttribute), typeof(CreateBookViewModel));

            Assert.That(hasAttribute, Is.True);
        }

        [Test]
        public void Create_PostsModelStateIsInvalid_ReturnsViewWithPassedMessage()
        {
            controller.ModelState.AddModelError("fel", "felet");
            var viewModel = new CreateBookViewModel();
            var result = (ViewResult)controller.Create(viewModel);

            Assert.That(result.ViewName, Is.EqualTo(""));
            Assert.That(result.Model, Is.SameAs(viewModel));
        }

        [Test]
        public void Create_OnCreated_RedirectToCreatedBookViewPage()
        {
            var book = new Mock<IBook>();
            book.Setup(l => l.Id).Returns(1);
            commandMock.Setup(c => c.Execute(It.IsAny<CreateBookCommand>())).Returns(new CommandResult<IBook>(book.Object));

            var result = (RedirectToRouteResult)controller.Create(new CreateBookViewModel());

            Assert.That(result.RouteValues["action"], Is.EqualTo("Index"));
            Assert.That(result.RouteValues["controller"], Is.Null);
            Assert.That(result.RouteValues["id"], Is.EqualTo(1));
        }

        [Test]
        public void Create_OnFailedToCreate_ReturnsViewWithErrors()
        {
            commandMock.Setup(c => c.Execute(It.IsAny<CreateBookCommand>())).Returns(new CommandResult<IBook>("fel"));

            var result = (ViewResult)controller.Create(new CreateBookViewModel());

            Assert.That(result.ViewName, Is.EqualTo(""));
            Assert.That(controller.ModelState.First().Value.Errors[0].ErrorMessage, Is.EqualTo("fel"));
        }

        [Test]
        public void Index_Always_ReturnsIndexView()
        {
            var result = (ViewResult)controller.Index(1);

            Assert.That(result.ViewName, Is.EqualTo(""));
        }

        //[Test]
        //public void Index_Always_ReturnsHomeViewModel()
        //{
        //    var model = new BookViewModel(Mock.Of<IBook>());
        //    storeMock.Setup(s => s.Execute(It.IsAny<BookQuery>())).Returns(model);

        //    var result = (ViewResult)controller.Index(1);

        //    Assert.That(result.Model, Is.SameAs(model));
        //}     
    }
}
