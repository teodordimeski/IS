using Domain.Dto;
using Domain.Models;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class ConsultationMapper
{
    public ConsultationDto ToDto(ConsultationRequest request) =>
        new(request.StartTime, request.EndTime, request.RoomId);

    public ConsultationResponse ToResponse(Consultation consultation) =>
        new(consultation.Id, consultation.StartTime, consultation.EndTime,
            consultation.RoomId, consultation.Room?.Name ?? string.Empty);
}
