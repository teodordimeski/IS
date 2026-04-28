using Domain.Dto;
using Domain.Models;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class RoomService : IRoomService
{
    private readonly IRepository<Room> _repo;

    public RoomService(IRepository<Room> repo)
    {
        _repo = repo;
    }

    public async Task<Room> GetByIdNotNullAsync(Guid id)
    {
        var entity = await _repo.GetAsync(r => r, predicate: r => r.Id == id);
        return entity ?? throw new KeyNotFoundException($"Room {id} not found.");
    }

    public Task<List<Room>> GetAllAsync()
    {
        return _repo.GetAllAsync(r => r);
    }

    public async Task<Room> CreateAsync(RoomDto dto)
    {
        var entity = new Room
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Capacity = dto.Capacity
        };

        return await _repo.InsertAsync(entity);
    }

    public async Task<Room> UpdateAsync(Guid id, RoomDto dto)
    {
        var entity = await GetByIdNotNullAsync(id);

        entity.Name = dto.Name;
        entity.Capacity = dto.Capacity;

        return await _repo.UpdateAsync(entity);
    }

    public async Task<Room> DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdNotNullAsync(id);
        return await _repo.DeleteAsync(entity);
    }

    public Task<PaginatedResult<Room>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return _repo.GetAllPagedAsync(r => r, pageNumber, pageSize);
    }
}
