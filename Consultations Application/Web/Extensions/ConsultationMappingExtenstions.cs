using Domain.Dto;
using Domain.Models;
using Web.Request;
using Web.Response;

namespace Web.Extensions;

public static class ConsultationMappingExtenstions
{
    
    public static ConsultationResponse ToResponse(this Consultation consultation)
    {
        return new ConsultationResponse(
            ConsultationId: consultation.Id,
            StartTime: consultation.StartTime,
            EndTime: consultation.EndTime,
            RoomId: consultation.RoomId,
            RoomName: consultation.Room.Name
        );
    }

    public static List<ConsultationResponse> ToResponse(this List<Consultation> consultations)
    {
        return consultations.Select(x => x.ToResponse()).ToList();
    }
    
    
    public static ConsultationWithAttendancesResponse ToConsultationWithAttendancesResponse(this Consultation consultation)
    {
        
        return new ConsultationWithAttendancesResponse(
            ConsultationId: consultation.Id,
            StartTime: consultation.StartTime,
            EndTime: consultation.EndTime,
            RoomId: consultation.RoomId,
            RoomName: consultation.Room.Name,
            Attendances: consultation.Attendances.Select(x => x.ToResponse()).ToList()
        );
    }

    public static ConsultationDto ToDto(this CreateOrUpdateConsultationRequest request)
    {
        return new ConsultationDto()
        {
            EndTime = request.EndTime,
            StartTime = request.StartTime,
            RoomId = request.RoomId,
        };
    }
    
    public static PaginatedResponse<ConsultationWithAttendancesResponse> ToPaginatedResponse(this PaginatedResult<Consultation> consultations)
    {
        return consultations.ToPaginatedResponse(x => x.ToConsultationWithAttendancesResponse());
    }
    
}