using Domain.Dto;
using Domain.Models;
using Service.Interface;
using Web.Extensions;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class CarMapper
{
    private readonly ICarService _service;

    public CarMapper(ICarService service)
    {
        _service = service;
    }


    public async Task<CarResponse> GetByIdNotNullAsync(Guid id)
    {
        var result =await _service.GetByIdNotNullAsync(id);
        return result.ToResponse();
    }

    public async Task<List<CarResponse>> GetAllAsync(string? LocationName)
    {
        var result = await _service.GetAllAsync(LocationName);
        return result.ToResponse();
    }

    public async Task<CarResponse> CreateAsync(CreateOrUpdateCarRequest request)
    {
        var dto = request.ToDto();
        var result =await _service.CreateAsync(dto);
        return result.ToResponse();
    }

    public async Task<CarResponse> UpdateAsync(Guid id,CreateOrUpdateCarRequest request)
    {
        var dto = request.ToDto();
        var result = await _service.UpdateAsync(id,dto);
        return result.ToResponse();
    }

    public async Task<CarResponse> DeleteByIdAsync(Guid id)
    {
       var result = await _service.DeleteByIdAsync(id);
       return result.ToResponse();
    }

    public async Task<PaginatedResponse<CarWithRentalsResponse>> GetPagedAsync(int pageNumber, int pageSize)
    {
        var result = await _service.GetPagedAsync(pageNumber, pageSize);
        return result.ToPaginatedResponse();
    }
}