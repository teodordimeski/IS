using Domain.Dto;
using Domain.Models;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class AttendanceMapper
{
    public AttendanceDto ToDto(AttendanceRequest request) =>
        new(request.Comment, request.Status, request.UserId, request.RoomId, request.ConsultationId);

    public AttendanceResponse ToResponse(Attendance attendance) =>
        new(attendance.Id, attendance.Comment, attendance.Status, attendance.UserId,
            attendance.RoomId, attendance.Room?.Name ?? string.Empty, attendance.ConsultationId);
}
