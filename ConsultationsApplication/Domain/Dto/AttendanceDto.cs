using Domain.Enums;

namespace Domain.Dto;

public record AttendanceDto(string? Comment, Status Status, string UserId, Guid RoomId, Guid ConsultationId);
