using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Mapper;
using Web.Request;

namespace Web.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ConsultationsController : ControllerBase
{
    public readonly ConsultationMapper _mapper;

    public ConsultationsController(ConsultationMapper mapper)
    {
        _mapper = mapper;
    }
    
    //http://localhost:5063/api/consultations/D885F434-F6E9-417B-AEF1-7089E26B369E
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdNotNullAsync([FromRoute] Guid id)
    {
        var result = await _mapper.GetByIdNotNullAsync(id);
        return Ok(result);
    }
    
    //http://localhost:5063/api/consultations?dateAfter=2024-01-01
    public async Task<IActionResult> GetAllAsync([FromQuery] string? dateAfter)
    {
        var result = await _mapper.GetAllAsync(dateAfter);
        return Ok(result);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateConsultationRequest request)
    {
        var result = await _mapper.CreateAsync(request);
        return Ok(result);
    }
    
    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateOrUpdateConsultationRequest request)
    {
        var result = await _mapper.UpdateAsync(id, request);
        return Ok(result);
    }
    
    
    //"http://localhost:5063/api/Consultations/paged?pageNumber=1&pageSize=10"
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAsync([FromQuery] PaginatedRequest request)
    {
        var result = await _mapper.GetPagedAsync(request.PageNumber, request.PageSize);
        return Ok(result);
    }
    
    
    
    //http://localhost:5063/api/consultations/D885F434-F6E9-417B-AEF1-7089E26B369E 
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
    {
        var result = await _mapper.DeleteByIdAsync(id);
        return Ok(result);
    }
    
    
}