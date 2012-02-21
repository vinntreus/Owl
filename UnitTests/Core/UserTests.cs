using Core.Users;
using NUnit.Framework;
using Web.Models;

namespace UnitTests.Core
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void Create_ShouldHashPassword()
        {
            var user = User.Create(new AddUserMessage {Password = "arne"});

            Assert.That(user.Password, Is.Not.EqualTo("arne"));
        }
    }
}