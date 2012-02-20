using System;

namespace Core.Users
{
    public class CreateUserException : Exception
    {
        public CreateUserException(string message) : base(message){}
    }
}