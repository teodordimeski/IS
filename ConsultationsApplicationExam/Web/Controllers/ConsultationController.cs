using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Web.Mapper;
using Web.Request;
using Web.Response;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ConsultationController: ControllerBase
{
    private readonly ConsultationMapper _consultationMapper;

    public ConsultationController(ConsultationMapper consultationMapper)
    {
        _consultationMapper = consultationMapper;
    }
    
    [HttpPost]
    public async Task<IActionResult> InsertAsync([FromBody] ConsultationRequest request)
    {
        var result = await _consultationMapper.InsertAsync(request);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<List<ConsultationResponse>> GetAllAsync([FromQuery] String? roomNumber,[FromQuery] DateOnly? date)
    {
        return await _consultationMapper.GetAllAsync(roomNumber, date);
         
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id,[FromBody] ConsultationRequest request)
    {
        var result = await _consultationMapper.UpdateAsync(id, request);
        return Ok(result);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute]  Guid id)
    {
        var result = await _consultationMapper.DeleteAsync(id);
        return Ok(result);
    }
    
    [HttpGet("paged")]
    public async Task<PaginatedResponse<ConsultationBasicResponse>> GetPagedAsync([FromQuery] PaginatedRequest request)
    {
        return await _consultationMapper.GetPagedAsync(request);
    }
    
}