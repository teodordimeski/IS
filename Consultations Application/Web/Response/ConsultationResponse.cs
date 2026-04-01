namespace Web.Response;

public record ConsultationResponse
(
    Guid ConsultationId, 
    DateTime StartTime, 
    DateTime EndTime,
    Guid RoomId,
    String RoomName
);