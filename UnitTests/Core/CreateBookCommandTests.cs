using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Books;
using Core.Users;
using Moq;
using NUnit.Framework;
using Raven.Client;

namespace UnitTests.Core
{
    [TestFixture]
    public class CreateBookCommandTests
    {
        private Mock<IDocumentSession> sessionMock;

        [SetUp]
        public void Setup()
        {
            sessionMock = new Mock<IDocumentSession>();
        }

        [Test]
        public void CreateLibraryCommandExecute_Always_StoreAndSaveChanges()
        {
            var message = new CreateBookMessage { Title = "Books" };
            var command = GetCommandWithSession(message);

            command.Execute();

            sessionMock.Verify(s => s.Store(It.Is<Book>(l => l.Title == "Books")));
            sessionMock.Verify(s => s.SaveChanges());
        }

        [Test]
        public void CreateLibraryCommandExecute_Always_SetsCurrentUserAsCreator()
        {
            var message = new CreateBookMessage { Title = "Books" };
            var command = GetCommandWithSession(message);
            var currentUser = new User();
            command.FakeCurrentUser = currentUser;

            command.Execute();

            sessionMock.Verify(s => s.Store(It.Is<Book>(l => l.Creator == currentUser)));
        }

        [Test]
        public void CreateLibraryCommandExecute_WhenSuccess_ReturnsCommandResultWithSuccess()
        {
            var message = new CreateBookMessage { Title = "Books" };
            var command = GetCommandWithSession(message);

            var result = command.Execute();

            Assert.That(result.IsSuccess(), Is.True);
        }

        private TestableBookCommand GetCommandWithSession(ICreateBookMessage message)
        {
            var command = new TestableBookCommand(message);
            command.Session = sessionMock.Object;
            return command;
        }

        private class TestableBookCommand : CreateBookCommand
        {
            public TestableBookCommand(ICreateBookMessage message)
                : base(message)
            {

            }
            public User FakeCurrentUser { get; set; }

            public override User CurrentUser
            {
                get
                {
                    return FakeCurrentUser;
                }
            }
        }

        private class CreateBookMessage : ICreateBookMessage
        {
            public string Title { get; set; }
            public int LibraryId { get; set; }
            public string Description { get; set; }
            public string Author { get; set; }
            public DateTime? Published { get; set; }
            public string Tags { get; set; }
            public string CoverSource { get; set; }
        }
    }
}
