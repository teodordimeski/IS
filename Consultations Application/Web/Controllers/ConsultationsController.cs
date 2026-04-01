using Microsoft.AspNetCore.Mvc;
using Web.Mapper;

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

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdNotNullAsync([FromRoute] Guid id)
    {
        var result = await _mapper.GetByIdNotNullAsync(id);
        return Ok(result);
    }
    
}