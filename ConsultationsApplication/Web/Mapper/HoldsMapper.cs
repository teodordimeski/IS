using Domain.Dto;
using Domain.Models;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class HoldsMapper
{
    public HoldsDto ToDto(HoldsRequest request) =>
        new(request.ConsultationId, request.UserId);

    public HoldsResponse ToResponse(Holds holds) =>
        new(holds.Id, holds.ConsultationId, holds.UserId);
}
