using Domain.Models;
using Web.Response;

namespace Web.Extensions;

public static class AttendanceExtensions
{
    public static AttendanceBasicResponse ToBasicResponse(this Attendance attendance)
    {
        return new AttendanceBasicResponse(
            attendance.Id,
            attendance.User.FirstName,
            attendance.User.LastName
        );
    }

    public static AttendanceResponse ToResponse(this Attendance attendance)
    {
        return new AttendanceResponse(
            attendance.Id,
            attendance.UserId,
            attendance.User.FirstName,
            attendance.User.LastName,
            attendance.Status.ToString(),
            attendance.Comment
        );
    }
}