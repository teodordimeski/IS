using Microsoft.AspNetCore.Mvc;
using Web.Mapper;
using Web.Request;
using Web.Response;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AttendanceController:Controller
{
    private readonly AttendanceMapper _mapper;

    public AttendanceController(AttendanceMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterAsync([FromBody] AttendanceRequest request)
    {
        var result = await _mapper.RegisterAsync(request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _mapper.DeleteAsync(id);
        return Ok();
    }
    
   
    [HttpGet("consultation/{id}")]
    public async Task<List<AttendanceResponse>> GetAllByConsultationIdAsync([FromRoute] Guid id)
    {
        return await _mapper.GetAllByConsultationIdAsync(id);
        
    }

    [HttpPatch("{id}/mark-as-absent")]
    public async Task<IActionResult> MarkAsAbsentAsync([FromRoute] Guid id)
    {
        var result = await _mapper.MarkAsAbsentAsync(id);
        return Ok();
    }
    
    [HttpPost("{id}/cancelation-reason")]
    public async Task<IActionResult> UploadReasonByIdInFileSystemAsync([FromRoute] Guid id,[FromForm] IFormFile file)
    {
        var result = await _mapper.UploadReasonByIdInFileSystemAsync(id, file);
        return Ok(result);
    }
    
    
    
    
    
}