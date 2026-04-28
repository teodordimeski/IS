using Domain.Dto;
using Domain.Models;

namespace Service.Interface;

public interface IRoomService
{
    Task<Room> GetByIdNotNullAsync(Guid id);
    Task<List<Room>> GetAllAsync();
    Task<Room> CreateAsync(RoomDto dto);
    Task<Room> UpdateAsync(Guid id, RoomDto dto);
    Task<Room> DeleteByIdAsync(Guid id);
    Task<PaginatedResult<Room>> GetPagedAsync(int pageNumber, int pageSize);
}
