namespace Web.Response;

public record ConsultationBasicResponse(
    Guid Id,
    Guid RoomId,
    DateTime StartTime,
    DateTime EndTime
    );

