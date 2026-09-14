namespace ShortURL.Exceptions;

public class UserTierDoesNotExsistException : Exception
{
    public UserTierDoesNotExsistException()
        : base("User tier is invalid.")
    {
    }
}