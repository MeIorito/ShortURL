namespace ShortURL.DTOs;

public class DeleteUrlByIdResponseDto
{
    public DeleteUrlByIdResponseDto(bool ack, long delCount)
    {
        IsAcknowledged = ack;
        DeletedCount = delCount;
    }

    public bool IsAcknowledged { get; set; }
    public long DeletedCount { get; set; }
}