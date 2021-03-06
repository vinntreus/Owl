﻿using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Sessions;
using Core.Users;
using Moq;
using NUnit.Framework;
using Raven.Client;

namespace UnitTests.Core
{
	[TestFixture]
	public class CreateSessionCommandTests
	{	

		[Test]
		public void Execute_UserDoesNotExist_ReturnFalse()
		{
			var message = Mock.Of<ICreateSessionMessage>();
			var sessionCommand = new TestableCreateSessionCommand(message);

			var result = sessionCommand.Execute();

			Assert.That(result.IsSuccess(), Is.False);
		}

		[Test]
		public void Execute_UserIsAuthenticated_ReturnTrue()
		{
			var sessionValues = new CreateSessionMessage { Username = "a", Password = "b" };
			var sessionCommand = new TestableCreateSessionCommand(sessionValues);
			sessionCommand.Users.Add(User.Create(sessionValues));

			var result = sessionCommand.Execute();

			Assert.That(result.IsSuccess(), Is.True);
		}

		private class TestableCreateSessionCommand : CreateSessionCommand
		{
			public TestableCreateSessionCommand(ICreateSessionMessage message)
				: base(message)
			{
				Users = new List<User>();
			}
			public IList<User> Users { get; set; }

			public override IQueryable<T> All<T>()
			{
				return Users.Cast<T>().AsQueryable<T>();
			}
		}

		private class CreateSessionMessage : ICreateSessionMessage, ICreateUserMessage
		{
			public string Username { get; set; }

			public string Password { get; set; }
		}

	}
}
