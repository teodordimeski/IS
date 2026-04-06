using Domain.Dto;
using Service.Interface;
using Web.Extensions;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class AttendanceMapper
{
    private readonly IAttendanceService _attendanceService;
    private readonly IFileUploadService _fileUploadService;

    public AttendanceMapper(IAttendanceService attendanceService, IFileUploadService fileUploadService)
    {
        _attendanceService = attendanceService;
        _fileUploadService = fileUploadService;
    }
    
    public async Task<AttendanceResponse> RegisterAsync(AttendanceRequest request)
    {
        var result = await _attendanceService.CreateAsync(new AttendanceDto()
        {
            Comment = request.Comment,
            ConsultationId = request.ConsultationId,
            UserId = request.UserId,
            RoomId = request.RoomId,
        });
        return result.ToResponse();
    }

    public async Task<AttendanceResponse> DeleteAsync(Guid id)
    {
        var result = await _attendanceService.DeleteByIdAsync(id);
        return result.ToResponse();
    }

    public async Task<List<AttendanceResponse>> GetAllByConsultationIdAsync(Guid id)
    {
        var result = await _attendanceService.FindByConsultationIdAsync(id);
        return result.Select(x => x.ToResponse()).ToList();
    }

    public async Task<AttendanceResponse> MarkAsAbsentAsync(Guid id)
    {
        var result = await _attendanceService.MarkAsAbsent(id);
        return result.ToResponse();
    }

    public async Task<AttendanceResponse> UploadReasonByIdInFileSystemAsync(Guid id, IFormFile file)
    {
        
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        
        var path = await _fileUploadService.UploadFileAsync(
            ms.ToArray(),
            file.FileName
        );
        
        var result = await _attendanceService.UpdateReasonPathByIdAsync(id, path);
        
        return result.ToResponse();
    }
    
    
    
}