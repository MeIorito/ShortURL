namespace ShortURL.Models;

public class Click
{
    private Click() { }

    public Click(
        Guid urlId,
        DateTime timeStamp,
        string ipAddress,
        string? userAgent,
        string? referer)
    {
        UrlId = urlId;
        TimeStamp = timeStamp;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        Referer = referer;
    }

    public Guid Id { get; private set; }

    public Guid UrlId { get; private set; }

    public DateTime TimeStamp { get; private set; }

    public string IpAddress { get; private set; } = string.Empty;

    public string? UserAgent { get; private set; }

    public string? Referer { get; private set; }
}