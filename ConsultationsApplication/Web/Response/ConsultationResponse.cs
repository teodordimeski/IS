namespace Web.Response;

public record ConsultationResponse(Guid Id, DateTime StartTime, DateTime EndTime, Guid RoomId, string RoomName);
