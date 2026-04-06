using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<Attendance> _attendanceRepository;
    private readonly IConsultationService _consultationService;

    public AttendanceService(IRepository<Attendance> attendanceRepository, IConsultationService consultationService)
    {
        _attendanceRepository = attendanceRepository;
        _consultationService = consultationService;
    }

    public async Task<Attendance> GetByIdNotNullAsync(Guid id)
    {
        var result = await GetByIdAsync(id);

        if (result == null)
        {
            throw new InvalidOperationException($"Attendance with id {id} not found");
        }
        return result;
    }

    public async Task<Attendance?> GetByIdAsync(Guid id)
    {
        return await _attendanceRepository.GetAsync(selector:x=>x, 
            predicate:x=>x.Id==id,
            include: x=>x.Include(x=>x.User).Include(x=>x.Consultation));
    }

    public async Task<List<Attendance>> GetAllAsync(string? dateAfter)
    {
        return await _attendanceRepository.GetAllAsync(selector:x=>x,
            predicate: x =>(dateAfter==null ||  x.Consultation.StartTime > DateTime.Parse(dateAfter)),
            include:x=>x.Include(x=>x.User).Include(x=>x.Consultation));
    }

    public async Task<Attendance> CreateAsync(AttendanceDto dto)
    {
        var attendance = new Attendance()
        {
            Comment = dto.Comment,
            UserId = dto.UserId,
            RoomId = dto.RoomId,
            ConsultationId = dto.ConsultationId,
            Status = Status.Registered
        };
        
        var result= await _attendanceRepository.InsertAsync(attendance);
        await _consultationService.AddOrRemoveReigstration(dto.ConsultationId, 1);
        
        return result;
        
    }

    public async Task<Attendance> UpdateAsync(Guid id, AttendanceDto dto)
    {
        var result =await GetByIdNotNullAsync(id);
        result.ConsultationId = dto.ConsultationId;
        result.UserId = dto.UserId;
        result.RoomId = dto.RoomId;
        result.Comment = dto.Comment;
        result.ConsultationId = dto.ConsultationId;
        
        return await _attendanceRepository.UpdateAsync(result);
        
    }

    public async Task<Attendance> DeleteByIdAsync(Guid id)
    {
        var attendance =await GetByIdNotNullAsync(id);
        if(attendance.Consultation.StartTime < DateTime.Now.AddHours(1))
        {
            throw new InvalidOperationException("Cannot delete attendance for this consultation");
        }
        var result = await _attendanceRepository.DeleteAsync(attendance);
        await _consultationService.AddOrRemoveReigstration(result.ConsultationId, -1);
        return result;
    }

    public async Task<PaginatedResult<Attendance>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return await _attendanceRepository.GetAllPagedAsync(selector:x=>x, 
            pageNumber: pageNumber, 
            pageSize: pageSize,
            include: x=>x.Include(x=>x.User).Include(x=>x.Consultation));
    }

    public async Task<Attendance> UpdateReasonPathByIdAsync(Guid id, string path)
    {
        var result = await GetByIdNotNullAsync(id);
        result.CancellationReasonDocumentPath = path;
        return await _attendanceRepository.UpdateAsync(result);
    }

    public async Task<List<Attendance>> FindByConsultationIdAsync(Guid id)
    {
        return await _attendanceRepository.GetAllAsync(selector:x=>x, 
            predicate:x=>x.ConsultationId==id,
            include:x=>x.Include(x=>x.User));
    }

    public async Task<Attendance> MarkAsAbsent(Guid id)
    {
        var result = await GetByIdNotNullAsync(id);
        result.Status = Status.Absent;
        return await _attendanceRepository.UpdateAsync(result);
    }
}