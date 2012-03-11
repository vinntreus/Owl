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
    }
}
