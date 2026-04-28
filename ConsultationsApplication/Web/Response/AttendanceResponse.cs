using Domain.Enums;

namespace Web.Response;

public record AttendanceResponse(Guid Id, string? Comment, Status Status, string UserId, Guid RoomId, string RoomName, Guid ConsultationId);
