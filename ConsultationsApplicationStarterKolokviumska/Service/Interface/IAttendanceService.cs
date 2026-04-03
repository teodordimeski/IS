using Domain.Dto;
using Domain.Models;

namespace Service.Interface;

public interface IAttendanceService
{
    Task<Attendance> GetByIdNotNullAsync(Guid id);
    Task<Attendance?> GetByIdAsync(Guid id);
    Task<List<Attendance>> GetAllAsync(Guid? consultationId);
    Task<Attendance> CreateAsync(AttendanceDto dto);
    Task<Attendance> DeleteByIdAsync(Guid id);
    Task<Attendance> UpdateReasonPathByIdAsync(Guid id, string path);
}