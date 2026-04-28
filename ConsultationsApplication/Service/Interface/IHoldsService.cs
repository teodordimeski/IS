using Domain.Dto;
using Domain.Models;

namespace Service.Interface;

public interface IHoldsService
{
    Task<Holds> GetByIdNotNullAsync(Guid id);
    Task<List<Holds>> GetAllAsync(Guid? consultationId, string? userId);
    Task<Holds> CreateAsync(HoldsDto dto);
    Task<Holds> UpdateAsync(Guid id, HoldsDto dto);
    Task<Holds> DeleteByIdAsync(Guid id);
    Task<PaginatedResult<Holds>> GetPagedAsync(int pageNumber, int pageSize);
}
