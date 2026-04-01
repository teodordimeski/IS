using Domain.Models;
using Web.Response;

namespace Web.Extensions;

public static class AttendanceMappingExtensions
{
    public static AttendanceResponse ToResponse(this Attendance attendance)
    {
        return new AttendanceResponse(
            AttendanceId: attendance.Id,
            UserId: attendance.UserId,
            UserName: attendance.User.FirstName,
            UserSurname: attendance.User.LastName,
            Status: attendance.Status.ToString()
        );
    }


    public static List<AttendanceResponse> ToResponse(this List<Attendance> attendances)
    {
        return attendances.Select(x=> x.ToResponse()).ToList();
    }

}
