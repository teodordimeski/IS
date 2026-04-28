using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class ConsultationService : IConsultationService
{
    private readonly IRepository<Consultation> _repo;

    public ConsultationService(IRepository<Consultation> repo)
    {
        _repo = repo;
    }

    public async Task<Consultation> GetByIdNotNullAsync(Guid id)
    {
        var entity = await _repo.GetAsync(
            c => c,
            predicate: c => c.Id == id,
            include: q => q.Include(c => c.Room));

        return entity ?? throw new KeyNotFoundException($"Consultation {id} not found.");
    }

    public Task<List<Consultation>> GetAllAsync(string? roomName)
    {
        return _repo.GetAllAsync(
            c => c,
            predicate: roomName == null ? null : c => c.Room.Name == roomName,
            include: q => q.Include(c => c.Room));
    }

    public async Task<Consultation> CreateAsync(ConsultationDto dto)
    {
        var entity = new Consultation
        {
            Id = Guid.NewGuid(),
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            RoomId = dto.RoomId
        };

        return await _repo.InsertAsync(entity);
    }

    public async Task<Consultation> UpdateAsync(Guid id, ConsultationDto dto)
    {
        var entity = await GetByIdNotNullAsync(id);

        entity.StartTime = dto.StartTime;
        entity.EndTime = dto.EndTime;
        entity.RoomId = dto.RoomId;

        return await _repo.UpdateAsync(entity);
    }

    public async Task<Consultation> DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdNotNullAsync(id);
        return await _repo.DeleteAsync(entity);
    }

    public Task<PaginatedResult<Consultation>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return _repo.GetAllPagedAsync(
            c => c,
            pageNumber,
            pageSize,
            include: q => q.Include(c => c.Room));
    }
}
