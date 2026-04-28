using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class HoldsService : IHoldsService
{
    private readonly IRepository<Holds> _repo;

    public HoldsService(IRepository<Holds> repo)
    {
        _repo = repo;
    }

    public async Task<Holds> GetByIdNotNullAsync(Guid id)
    {
        var entity = await _repo.GetAsync(
            h => h,
            predicate: h => h.Id == id,
            include: q => q.Include(h => h.Consultation));

        return entity ?? throw new KeyNotFoundException($"Holds {id} not found.");
    }

    public Task<List<Holds>> GetAllAsync(Guid? consultationId, string? userId)
    {
        return _repo.GetAllAsync(
            h => h,
            predicate: h =>
                (consultationId == null || h.ConsultationId == consultationId) &&
                (userId == null || h.UserId == userId),
            include: q => q.Include(h => h.Consultation));
    }

    public async Task<Holds> CreateAsync(HoldsDto dto)
    {
        var entity = new Holds
        {
            Id = Guid.NewGuid(),
            ConsultationId = dto.ConsultationId,
            UserId = dto.UserId
        };

        return await _repo.InsertAsync(entity);
    }

    public async Task<Holds> UpdateAsync(Guid id, HoldsDto dto)
    {
        var entity = await GetByIdNotNullAsync(id);

        entity.ConsultationId = dto.ConsultationId;
        entity.UserId = dto.UserId;

        return await _repo.UpdateAsync(entity);
    }

    public async Task<Holds> DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdNotNullAsync(id);
        return await _repo.DeleteAsync(entity);
    }

    public Task<PaginatedResult<Holds>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return _repo.GetAllPagedAsync(
            h => h,
            pageNumber,
            pageSize,
            include: q => q.Include(h => h.Consultation));
    }
}
