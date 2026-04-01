namespace Web.Response;

public record AttendanceResponse(
    Guid AttendanceId,
    String UserId,
    String UserName,
    String UserSurname,
    String Status
    );