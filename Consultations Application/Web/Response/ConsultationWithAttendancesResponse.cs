namespace Web.Response;

public record ConsultationWithAttendancesResponse(
    Guid ConsultationId, 
    DateTime StartTime, 
    DateTime EndTime,
    Guid RoomId,
    String RoomName,
    List<AttendanceResponse>  Attendances
    );