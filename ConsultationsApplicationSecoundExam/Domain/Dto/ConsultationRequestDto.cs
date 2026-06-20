namespace Domain.Dto;

public class ConsultationRequestDto
{
    public Guid ConsultationId { get; set; } 
    public string UserId { get; set; }
    public Guid RoomId { get; set; } 
    public string? Comment { get; set; }
}