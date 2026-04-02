using Microsoft.AspNetCore.Mvc;
using Web.Mapper;

namespace Web.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class CarsController :ControllerBase
{
    private readonly CarMapper _mapper;

    public CarsController(CarMapper mapper)
    {
        _mapper = mapper;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdNotNullAsync(Guid id)
    {
        var result = await _mapper.GetByIdNotNullAsync(id);
        return Ok(result);
    }
    
}