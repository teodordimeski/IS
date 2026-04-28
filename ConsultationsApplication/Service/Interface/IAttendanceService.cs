using Domain.Dto;
using Domain.Enums;
using Domain.Models;

namespace Service.Interface;

public interface IAttendanceService
{
    Task<Attendance> GetByIdNotNullAsync(Guid id);
    Task<List<Attendance>> GetAllAsync(Guid? consultationId, string? userId, Status? status);
    Task<Attendance> CreateAsync(AttendanceDto dto);
    Task<Attendance> UpdateAsync(Guid id, AttendanceDto dto);
    Task<Attendance> DeleteByIdAsync(Guid id);
    Task<PaginatedResult<Attendance>> GetPagedAsync(int pageNumber, int pageSize);
}
