using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class AttendanceService : IAttendanceService
{
    private readonly IRepository<Attendance> _repo;

    public AttendanceService(IRepository<Attendance> repo)
    {
        _repo = repo;
    }

    public async Task<Attendance> GetByIdNotNullAsync(Guid id)
    {
        var entity = await _repo.GetAsync(
            a => a,
            predicate: a => a.Id == id,
            include: q => q.Include(a => a.Room).Include(a => a.Consultation));

        return entity ?? throw new KeyNotFoundException($"Attendance {id} not found.");
    }

    public Task<List<Attendance>> GetAllAsync(Guid? consultationId, string? userId, Status? status)
    {
        return _repo.GetAllAsync(
            a => a,
            predicate: a =>
                (consultationId == null || a.ConsultationId == consultationId) &&
                (userId == null || a.UserId == userId) &&
                (status == null || a.Status == status),
            include: q => q.Include(a => a.Room).Include(a => a.Consultation));
    }

    public async Task<Attendance> CreateAsync(AttendanceDto dto)
    {
        var entity = new Attendance
        {
            Id = Guid.NewGuid(),
            Comment = dto.Comment,
            Status = dto.Status,
            UserId = dto.UserId,
            RoomId = dto.RoomId,
            ConsultationId = dto.ConsultationId
        };

        return await _repo.InsertAsync(entity);
    }

    public async Task<Attendance> UpdateAsync(Guid id, AttendanceDto dto)
    {
        var entity = await GetByIdNotNullAsync(id);

        entity.Comment = dto.Comment;
        entity.Status = dto.Status;
        entity.UserId = dto.UserId;
        entity.RoomId = dto.RoomId;
        entity.ConsultationId = dto.ConsultationId;

        return await _repo.UpdateAsync(entity);
    }

    public async Task<Attendance> DeleteByIdAsync(Guid id)
    {
        var entity = await GetByIdNotNullAsync(id);
        return await _repo.DeleteAsync(entity);
    }

    public Task<PaginatedResult<Attendance>> GetPagedAsync(int pageNumber, int pageSize)
    {
        return _repo.GetAllPagedAsync(
            a => a,
            pageNumber,
            pageSize,
            include: q => q.Include(a => a.Room).Include(a => a.Consultation));
    }

    public async Task<List<Attendance>> GetAllThatShouldBeDeleted(DateTime date)
    {
        return await _repo.GetAllAsync(
            a => a,
            predicate: a => a.Consultation.CreatedAt < date && a.Status==Status.Absent,
            include: q => q.Include(a => a.Consultation));
    }

    public async Task<Attendance> DeleteByObject(Attendance attendance)
    {
        return await _repo.DeleteAsync(attendance);
    }
}
