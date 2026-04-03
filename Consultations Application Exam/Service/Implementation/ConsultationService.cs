using Domain.Dto;
using Domain.Models;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class ConsultationService:IConsultationService
{
    private readonly IRepository<Consultation> _repository;

    public ConsultationService(IRepository<Consultation> repository)
    {
        _repository = repository;
    }

    public async Task<Consultation> GetByIdNotNullAsync(Guid id)
    {
        var result = await _repository.GetAsync(
            selector: x => x,
            predicate: x => x.Id == id);

        if (result == null)
        {
            throw new InvalidOperationException();
        }
        return result;
    }

    public async Task<Consultation?> GetByIdAsync(Guid id)
    {
        return await _repository.GetAsync(
            selector: x => x,
            predicate: x => x.Id == id);
    }

    public async Task<List<Consultation>> GetAllAsync(string? roomName, DateOnly date)
    {
        if(roomName == null)
        {
            return await _repository.GetAllAsync(
                selector: x => x,
                predicate: x => x.StartTime.Date.Equals(date));
        }
        return await _repository.GetAllAsync(
            selector: x => x,
            predicate: x => x.Room.Name == roomName && DateOnly.FromDateTime(x.StartTime) == date);
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
        return await _repository.InsertAsync(consultation);
    }

    public async Task<Consultation> UpdateAsync(Guid id, DateTime startTime, DateTime endTime, Guid roomId)
    {
        var consultation =await GetByIdNotNullAsync(id);

        if (consultation.RegisteredStudents != 0)
        {
            throw new InvalidOperationException();
        }
        consultation.StartTime = startTime;
        consultation.EndTime = endTime;
        consultation.RoomId = roomId;
        return await _repository.UpdateAsync(consultation);
    }

    public async Task<Consultation> DeleteByIdAsync(Guid id)
    {
        var consultation =await GetByIdNotNullAsync(id);
        
        if (consultation.RegisteredStudents != 0)
        {
            throw new InvalidOperationException();
        }
        
        return await _repository.DeleteAsync(consultation);
    }

    public Task<PaginatedResult<Consultation>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return _repository.GetAllPagedAsync(
            selector: x => x,
            pageNumber: pageNumber,
            pageSize: pageSize);
    }
}