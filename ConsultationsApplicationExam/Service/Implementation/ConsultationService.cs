using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class ConsultationService : IConsultationService
{
    private readonly IRepository<Consultation> _consultationRepository;

    public ConsultationService(IRepository<Consultation> consultationRepository)
    {
        _consultationRepository = consultationRepository;
    }

    public async Task<Consultation> GetByIdNotNullAsync(Guid id)
    {
        var result = await GetByIdAsync(id);
        if (result == null)
        {
            throw new InvalidOperationException($"Consultation with ${id} not found");
        }
        return result;
    }

    public async Task<Consultation?> GetByIdAsync(Guid id)
    {
        return await _consultationRepository.GetAsync(selector:x=>x,
            predicate:x=>x.Id==id,
            include: x=>x.Include(x=>x.Room).Include(x=>x.Attendances).ThenInclude(a=>a.User));
    }

    public async Task<List<Consultation>> GetAllAsync(string? roomName, DateOnly? date)
    {
        return await _consultationRepository.GetAllAsync(selector:x=>x, 
            predicate:x=>(roomName == null || x.Room.Name==roomName) 
                         && (date == null || DateOnly.FromDateTime(x.StartTime) == date),
            include: x=>x.Include(x=>x.Room).Include(x=>x.Attendances).ThenInclude(a=>a.User));
    }

    public async Task<Consultation> CreateAsync(DateTime startTime, DateTime endTime, Guid roomId)
    {
        var consultation = new Consultation()
        {
            StartTime = startTime,
            EndTime = endTime,
            RoomId = roomId,
            RegisteredStudents = 0
        };
        return await _consultationRepository.InsertAsync(consultation);
    }

    public async Task<Consultation> UpdateAsync(Guid id, DateTime startTime, DateTime endTime, Guid roomId)
    {
        var toUpdate =await GetByIdNotNullAsync(id);
        
        if (toUpdate.RegisteredStudents > 0)
        {
            throw new InvalidOperationException("Cannot update consultation with registered students");
        }
        
        toUpdate.StartTime = startTime;
        toUpdate.EndTime = endTime;
        toUpdate.RoomId = roomId;
        
        return await _consultationRepository.UpdateAsync(toUpdate);
        
    }

    public async Task<Consultation> DeleteByIdAsync(Guid id)
    {
        var toDelete =await GetByIdNotNullAsync(id);
        
        if (toDelete.RegisteredStudents > 0)
        {
            throw new InvalidOperationException("Cannot update consultation with registered students");
        }
        
        return await _consultationRepository.DeleteAsync(toDelete);
    }

    public async Task<PaginatedResult<Consultation>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _consultationRepository.GetAllPagedAsync(selector:x=>x, pageNumber: pageNumber, pageSize: pageSize, include: x=>x.Include(x=>x.Room).Include(x=>x.Attendances).ThenInclude(a=>a.User));
    }

    public async Task<Consultation> AddOrRemoveReigstration(Guid id, int param)
    {
        var result = await GetByIdNotNullAsync(id);
        result.RegisteredStudents += param;
        return await _consultationRepository.UpdateAsync(result);
    }
}