namespace Web.Response;

public record ConsultationResponse(
    Guid Id,
    DateOnly Date,
    Guid RoomId,
    String? RoomName,
    int RegisteredStudents,
    List<AttendanceBasicResponse> Attendances
    );
