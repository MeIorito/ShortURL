namespace ShortURL.Exceptions;

public class FailedValidationException : Exception
{
    public FailedValidationException(string message)
        : base(message)
    {
    }
}