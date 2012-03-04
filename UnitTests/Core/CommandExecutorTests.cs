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
		private Mock<IDocumentStore> storeMock;
		private CommandExecutor executor;

		[SetUp]
		public void Setup()
		{
			storeMock = new Mock<IDocumentStore>();
			executor = new CommandExecutor(storeMock.Object);
		}

		[Test]
		public void ExecuteCommand_ShouldCallExecuteOnCommand()
		{
			var command = new Mock<Command>();			

			executor.Execute(command.Object);

			command.Verify(c => c.Execute());
		}

		[Test]
		public void ExecuteCommand_ShouldSetStoreOnCommand()
		{
			var command = Mock.Of<Command>();

			executor.Execute(command);

			Assert.That(command.Store, Is.SameAs(storeMock.Object));
		}		
	}
}
