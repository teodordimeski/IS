using Domain.Dto;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class ConsultationService: IConsultationService
{
    private readonly IRepository<Consultation> _consultationRepository;

    public ConsultationService(IRepository<Consultation> consultationRepository)
    {
        _consultationRepository = consultationRepository;
    }

    public async Task<Consultation> GetByIdNotNullAsync(Guid id)
    {
        var result =  await _consultationRepository.GetAsync(
            selector: x =>x,
            predicate: x => x.Id == id);
        if (result == null)
        {
            throw new InvalidOperationException();
        }

        return result;
    }

    public async Task<List<Consultation>> GetAllAsync(string? dateAfter)
    {
        if (dateAfter == null)
        {
            return await _consultationRepository.GetAllAsync(
                selector: x => x);
        }
        
        return await _consultationRepository.GetAllAsync(
            selector: x => x,
            predicate: x =>  x.StartTime > DateTime.Parse(dateAfter));
    }

    public Task<Consultation> CreateAsync(ConsultationDto dto)
    {
        var newConsultation = new Consultation()
        {
            StartTime = dto.StartTime,
            EndTime = dto.EndTime,
            RoomId = dto.RoomId,
        };
        return _consultationRepository.InsertAsync(newConsultation);
    }

    public async Task<Consultation> UpdateAsync(Guid id, ConsultationDto dto)
    {
        var toUpdate = await GetByIdNotNullAsync(id);
        toUpdate.StartTime = dto.StartTime;
        toUpdate.EndTime = dto.EndTime;
        toUpdate.RoomId = dto.RoomId;
        
        return await _consultationRepository.UpdateAsync(toUpdate);
    }

    public async Task<Consultation> DeleteByIdAsync(Guid id)
    {
        var toDelete = await GetByIdNotNullAsync(id);
        return await _consultationRepository.DeleteAsync(toDelete);
    }

    public Task<PaginatedResult<Consultation>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return _consultationRepository.GetAllPagedAsync(
            selector: x => x,
            pageNumber: pageNumber,
            pageSize: pageSize,
            include: x=>x.Include(x=>x.Attendances));
    }
}