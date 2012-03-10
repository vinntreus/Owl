namespace Core.Users
{
    public interface ICreateUserMessage
    {
        string Username { get; }
        string Password { get; }
    }
}