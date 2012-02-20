namespace Core.Users
{
    public interface IAddUserMessage
    {
        string Username { get; }
        string Password { get; }
    }
}