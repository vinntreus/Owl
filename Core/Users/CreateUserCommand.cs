using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Users
{
    public class CreatedUser : IDomainEvent
    {
        public CreatedUser(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user");

            this.User = user;
        }

        public User User { get; private set; }
    }

	public class CreateUserCommand : Command<IUser>
	{
		private readonly IAddUserMessage message;

		public CreateUserCommand(IAddUserMessage message)
		{
			this.message = message;
		}

        public override CommandResult<IUser> Execute()
        {
            if (All<User>().Any(u => u.Username == message.Username))
                return new CommandResult<IUser>("User already exists");
            
            var user = User.Create(message);

            Session.Store(user);
            Session.SaveChanges();

            DomainEvents.Raise(new CreatedUser(user));

            return new CommandResult<IUser>(user);
        }     
    }   
}
