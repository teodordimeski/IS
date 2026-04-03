using Domain.Dto;
using Domain.Models;
using Web.Response;

namespace Web.Extensions;

public static class ConsultationExtensions
{
    public static ConsultationResponse ToResponse(this Consultation consultation)
    {
        return new ConsultationResponse(
            consultation.Id,
            DateOnly.FromDateTime(consultation.StartTime),
            consultation.RoomId,
            consultation.Room.Name,
            consultation.RegisteredStudents);
    }

    public static List<ConsultationResponse> ToResponse(this List<Consultation> consultation)
    {
        return consultation.Select(x => x.ToResponse()).ToList();
    }
    
    public static PaginatedResponse<ConsultationResponse> ToResponse(this PaginatedResult<Consultation> consultation)
    {
        return consultation.ToPaginatedResponse(x => x.ToResponse());
    }
    
}