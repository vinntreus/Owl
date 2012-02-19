using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Raven.Client;

namespace Core
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public static User Create(ICreateUser message)
        {
            return new User
            {
                Username = message.Username,
                Password = message.Password
            };
        }
    }
    public interface IHandleUsers
    {
        void Add(ICreateUser message);
        IEnumerable<IUser> All();
    }

    public class Users : IHandleUsers
    {
        private readonly IDocumentStore store;

        public Users(IDocumentStore store)
        {
            this.store = store;
        }

        public void Add(ICreateUser message)
        {
            var user = User.Create(message);

            using(var session = store.OpenSession())
            {
                if (session.Query<User>().Any(u => u.Username == message.Username))
                    throw new CreateUserException("User already exists");

                session.Store(user);

                session.SaveChanges();
            }
        }

        public IEnumerable<IUser> All()
        {
            using(var session = store.OpenSession())
            {
                return session.Query<User>().ToList();
            }
        }
    }

    public interface IUser
    {
        string Username { get; }
    }

    public class CreateUserException : Exception
    {
        public CreateUserException(string message) : base(message){}
    }

    public interface ICreateUser
    {
        string Username { get; }
        string Password { get; }
    }
}
