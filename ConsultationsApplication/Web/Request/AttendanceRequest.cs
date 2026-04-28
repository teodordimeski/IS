using Domain.Enums;

namespace Web.Request;

public record AttendanceRequest(string? Comment, Status Status, string UserId, Guid RoomId, Guid ConsultationId);
