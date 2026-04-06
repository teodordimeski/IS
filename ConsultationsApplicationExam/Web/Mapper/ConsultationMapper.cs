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

   public async Task<List<ConsultationResponse>> GetAllAsync(String? roomNumber, DateOnly? date)
   {
      var result = await _consultationService.GetAllAsync(roomNumber, date);
      return result.Select(x => x.ToResponse()).ToList();
   }

   public async Task<ConsultationBasicResponse> InsertAsync(ConsultationRequest request)
   {
      
      var result = await _consultationService.CreateAsync(request.StartDate, request.EndDate, request.RoomId);
      return result.ToBasicResponse();
   }

   public async Task<ConsultationBasicResponse> UpdateAsync(Guid id, ConsultationRequest request)
   {
      var result = await _consultationService.UpdateAsync(id, request.StartDate, request.EndDate, request.RoomId);
      return result.ToBasicResponse();
   }

   public async Task<ConsultationResponse> DeleteAsync(Guid id)
   {
      var result = await _consultationService.DeleteByIdAsync(id);
      return result.ToResponse();
   }
   
   public async Task<PaginatedResponse<ConsultationBasicResponse>> GetPagedAsync(PaginatedRequest request)
   {
      var result = await _consultationService.GetPagedAsync(request.PageNumber, request.PageSize);
      return result.ToPaginatedResponse(x => x.ToBasicResponse());
   }
   
   
}