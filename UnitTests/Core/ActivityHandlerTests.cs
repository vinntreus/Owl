using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Activities;
using Core.Sessions;
using Core.Users;
using Moq;
using NUnit.Framework;
using Raven.Client;

namespace UnitTests.Core
{
    [TestFixture]
    public class ActivityHandlerTests
    {
        public IDocumentSession GetSessionFake()
        {
            return Mock.Of<IDocumentSession>();
        }

        [Test]
        public void CreatedSessionActivity_Always_HaveCorrectText()
        {
            var handler = new CreatedSessionActivity(null);

            var result = handler.GetActivityText(new CreatedSession(new User() { Username = "arne" }));

            Assert.That(result, Is.EqualTo("arne logged in"));
        }

        [Test]
        public void CreatedUserActivity_Always_HaveCorrectText()
        {
            var handler = new CreatedUserActivity(null);

            var result = handler.GetActivityText(new CreatedUser(new User() { Username = "arne" }));

            Assert.That(result, Is.EqualTo("arne was created"));
        }

    }
}
