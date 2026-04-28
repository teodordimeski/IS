namespace Domain.Dto;

public record ConsultationDto(DateTime StartTime, DateTime EndTime, Guid RoomId);
