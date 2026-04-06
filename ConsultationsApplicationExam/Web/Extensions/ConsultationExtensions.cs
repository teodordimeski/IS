using Domain.Models;
using Web.Response;

namespace Web.Extensions;

public static class ConsultationExtensions
{
    public static ConsultationBasicResponse ToBasicResponse(this Consultation consultation)
    {
        return new ConsultationBasicResponse(
            consultation.Id,
            consultation.RoomId,
            consultation.StartTime,
            consultation.EndTime
        );
    }

    public static ConsultationResponse ToResponse(this Consultation consultation)
    {
        return new ConsultationResponse(
            consultation.Id,
            DateOnly.FromDateTime(consultation.StartTime),
            consultation.RoomId,
            consultation.Room?.Name,
            consultation.RegisteredStudents,
            consultation.Attendances?.Select(x => x.ToBasicResponse()).ToList() ?? new List<AttendanceBasicResponse>()
        );
    }
    
}