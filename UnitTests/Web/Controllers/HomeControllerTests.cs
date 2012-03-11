using System.Collections.Generic;
using System.Web.Mvc;
using Core;
using Core.Libraries;
using Core.Queries;
using Moq;
using NUnit.Framework;
using Web.Controllers;

namespace UnitTests.Web.Controllers
{
    [TestFixture]
    public class HomeControllerTests
    {
        private HomeController controller;
        private Mock<IStore> storeMock;

        [SetUp]
        public void Setup()
        {
            storeMock = new Mock<IStore>();
            controller = new HomeController(storeMock.Object);
        }

        [Test]
        public void Index_Always_ReturnsIndexView()
        {
            var result = (ViewResult)controller.Index();

            Assert.That(result.ViewName, Is.EqualTo(""));
        }     

         [Test]
        public void Index_Always_ReturnsHomeViewModel()
        {
            var model = new HomeViewModel(new List<ActivityViewModel>(), new List<ILibrary>());
            storeMock.Setup(s => s.Execute(It.IsAny<HomeQuery>())).Returns(model);

            var result = (ViewResult)controller.Index();

            Assert.That(result.Model, Is.SameAs(model));
        }     

        
    }
}
