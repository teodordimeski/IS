using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Mapper;
using Web.Request;

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
    
    //http://localhost:5284/api/Cars/EC70755C-E56F-4F40-A745-214A31B2D70B
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdNotNullAsync(Guid id)
    {
        var result = await _mapper.GetByIdNotNullAsync(id);
        return Ok(result);
    }
    
    
    //http://localhost:5284/api/Cars?LocationName=Downtown%20Branch
    //http://localhost:5284/api/Cars
    
    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? LocationName)
    {
        var result = await _mapper.GetAllAsync(LocationName);
        return Ok(result);
    }

    //http://localhost:5284/api/Cars/paged?pageNumber=1&pageSize=10
    [HttpGet("paged")]
    public async Task<IActionResult> GetPagedAsync( [FromQuery] PaginatedRequest  paginatedRequest)
    {
        var result = await _mapper.GetPagedAsync(paginatedRequest.PageNumber, paginatedRequest.PageSize);
        return Ok(result);
    }
    
    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> CreateAsync([FromBody] CreateOrUpdateCarRequest request)
    {
        var result = await _mapper.CreateAsync(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    // [Authorize]
    public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] CreateOrUpdateCarRequest request)
    {
        var result = await _mapper.UpdateAsync(id, request);
        return Ok(result);
    }
    
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _mapper.DeleteByIdAsync(id);
        return Ok(result);
    }
}