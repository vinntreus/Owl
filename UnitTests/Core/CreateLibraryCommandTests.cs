using Core.Libraries;
using Core.Users;
using Moq;
using NUnit.Framework;
using Raven.Client;

namespace UnitTests.Core
{
    [TestFixture]
    public class CreateLibraryCommandTests
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
            var message = new CreateLibraryMessage{ Name = "Books" };
            var command = GetCommandWithSession(message);

            command.Execute();

            sessionMock.Verify(s => s.Store(It.Is<Library>(l => l.Name == "Books")));
            sessionMock.Verify(s => s.SaveChanges());
        }

        [Test]
        public void CreateLibraryCommandExecute_Always_SetsCurrentUserAsCreator()
        {
            var message = new CreateLibraryMessage{ Name = "Books" };
            var command = GetCommandWithSession(message);
            var currentUser = new User();
            command.FakeCurrentUser = currentUser;

            command.Execute();

            sessionMock.Verify(s => s.Store(It.Is<Library>(l => l.Creator == currentUser)));
        }

        [Test]
        public void CreateLibraryCommandExecute_WhenSuccess_ReturnsCommandResultWithSuccess()
        {
            var message = new CreateLibraryMessage { Name = "Books" };
            var command = GetCommandWithSession(message);
            
            var result = command.Execute();

            Assert.That(result.IsSuccess(), Is.True);
        }

        private TestableLibraryCommand GetCommandWithSession(ICreateLibraryMessage message)
        {
            var command = new TestableLibraryCommand(message);            
            command.Session = sessionMock.Object;
            return command;
        }

        private class TestableLibraryCommand : CreateLibraryCommand
        {
            public TestableLibraryCommand(ICreateLibraryMessage message) : base(message)
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

        private class CreateLibraryMessage : ICreateLibraryMessage
        {
            public string Name { get; set; }
        }
    }
}
