namespace Web.Request;

public record ConsultationRequest(
    DateTime startTime,
    DateTime endTime,
    Guid roomId
    );