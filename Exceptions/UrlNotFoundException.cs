namespace ShortURL.Exceptions;

public class UrlNotFoundException : Exception
{
    public UrlNotFoundException()
        : base("Url not found.")
    {
    }
}