using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Web.Mapper;
using Web.Request;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultationController : ControllerBase
{
    private readonly ConsultationMapper _mapper;

    public ConsultationController(ConsultationMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? roomName, [FromQuery] DateOnly date)
    {
        var result = _mapper.GetAllAsync(roomName, date);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,[FromBody] ConsultationRequest request)
    {
        var result = _mapper.UpdateAsync(id,  request);
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