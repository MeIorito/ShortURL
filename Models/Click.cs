namespace ShortURL.Models;


public class Click
{
   private Click() {} 

   public Click(Guid urlId, DateTime timeStamp, string ipAddress)
    {
        UrlId = urlId;
        TimeStamp = timeStamp;
        IpAddress = ipAddress;
    }

   public Guid Id { get; private set;}
   
   public Guid UrlId { get; private set;}

   public DateTime TimeStamp { get; set;}

   public required string IpAddress { get; set;}
}