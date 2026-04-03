using Domain.Dto;
using Domain.Models;
using Service.Interface;
using Web.Extensions;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class ConsultationMapper
{
    private readonly IConsultationService _consultationService;

    public ConsultationMapper(IConsultationService consultationService)
    {
        _consultationService = consultationService;
    }

    public async Task<ConsultationResponse> GetByIdNotNullAsync(Guid id)
    {
        var result = await _consultationService.GetByIdNotNullAsync(id);
        return result.ToResponse();
    }
    

    public async Task<List<ConsultationResponse>> GetAllAsync(string? roomName, DateOnly date)
    {
        var result =await _consultationService.GetAllAsync(roomName, date);
        return result.ToResponse();
    }

    public async Task<ConsultationResponse> CreateAsync(DateTime startTime, DateTime endTime, Guid roomId)
    {
        var result = await _consultationService.CreateAsync(startTime, endTime, roomId);
        return result.ToResponse();
    }

    public async Task<ConsultationResponse> UpdateAsync(Guid id, ConsultationRequest  request)
    {
        
        
        var result = await _consultationService.UpdateAsync(id, request.startTime, request.endTime, request.roomId);
        return result.ToResponse();
    }

    public async Task<ConsultationResponse> DeleteByIdAsync(Guid id)
    {
    var result = await _consultationService.DeleteByIdAsync(id);
    return result.ToResponse();
    }

    public async Task<PaginatedResponse<ConsultationResponse>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _consultationService.GetPagedAsync(pageNumber, pageSize);
        return result.ToPaginatedResponse(x => x.ToResponse());
    }
}