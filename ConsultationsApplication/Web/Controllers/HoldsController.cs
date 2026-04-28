using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;
using Web.Extensions;
using Web.Mapper;
using Web.Request;
using Web.Response;

namespace Web.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class HoldsController : ControllerBase
{
    private readonly IHoldsService _service;
    private readonly HoldsMapper _mapper;

    public HoldsController(IHoldsService service, HoldsMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<HoldsResponse>>> GetAll(
        [FromQuery] Guid? consultationId,
        [FromQuery] string? userId)
    {
        var items = await _service.GetAllAsync(consultationId, userId);
        return Ok(items.Select(_mapper.ToResponse).ToList());
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PaginatedResponse<HoldsResponse>>> GetPaged([FromQuery] PaginatedRequest request)
    {
        var result = await _service.GetPagedAsync(request.PageNumber, request.PageSize);
        return Ok(result.ToPaginatedResponse(_mapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<HoldsResponse>> GetById(Guid id)
    {
        var entity = await _service.GetByIdNotNullAsync(id);
        return Ok(_mapper.ToResponse(entity));
    }

    [HttpPost]
    public async Task<ActionResult<HoldsResponse>> Create([FromBody] HoldsRequest request)
    {
        var entity = await _service.CreateAsync(_mapper.ToDto(request));
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.ToResponse(entity));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<HoldsResponse>> Update(Guid id, [FromBody] HoldsRequest request)
    {
        var entity = await _service.UpdateAsync(id, _mapper.ToDto(request));
        return Ok(_mapper.ToResponse(entity));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<HoldsResponse>> Delete(Guid id)
    {
        var entity = await _service.DeleteByIdAsync(id);
        return Ok(_mapper.ToResponse(entity));
    }
}
