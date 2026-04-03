using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web.Mapper;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultationController : ControllerBase
{
    private readonly ConsultationMapper _mapper;
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? roomName, [FromQuery] DateOnly date)
    {
        var result = _mapper.GetAllAsync(roomName, date);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,[FromBody] DateTime startTime,[FromBody] DateTime endTime,[FromBody] Guid roomId)
    {
        var result = _mapper.UpdateAsync(id, startTime, endTime, roomId);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = _mapper.DeleteByIdAsync(id);
        return Ok(result);
    }
    
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAsync([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var result = _mapper.GetPagedAsync(pageNumber, pageSize);
        return Ok(result);
    }
    
    
}