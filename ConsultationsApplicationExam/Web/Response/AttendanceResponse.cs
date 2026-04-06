namespace Web.Response;

public record AttendanceResponse(
    Guid Id,
    String UserId,
    String? FirstName,
    String? LastName,
    String Status,
    String? Comment
    );