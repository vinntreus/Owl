using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;
using Moq;
using NUnit.Framework;
using Raven.Client;

namespace UnitTests.Core
{
	[TestFixture]
	public class CommandExecutorTests
	{
        private Mock<IDocumentSession> sessionMock;
		private CommandExecutor executor;

		[SetUp]
		public void Setup()
		{
			sessionMock = new Mock<IDocumentSession>();
			executor = new CommandExecutor(sessionMock.Object);
		}

		[Test]
		public void ExecuteCommand_ShouldCallExecuteOnCommand()
		{
			var command = new Mock<Command<bool>>();			

			executor.Execute(command.Object);

			command.Verify(c => c.Execute());
		}

		[Test]
		public void ExecuteCommand_ShouldSetStoreOnCommand()
		{
            var command = Mock.Of<Command<bool>>();

			executor.Execute(command);

			Assert.That(command.Session, Is.SameAs(sessionMock.Object));
		}		
	}
}
