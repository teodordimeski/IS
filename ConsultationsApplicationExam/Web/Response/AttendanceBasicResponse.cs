namespace Web.Response;

public record AttendanceBasicResponse(
    Guid Id,
    String? FirstName,
    String? LastName
    );