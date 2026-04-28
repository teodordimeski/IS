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
public class RoomController : ControllerBase
{
    private readonly IRoomService _service;
    private readonly RoomMapper _mapper;

    public RoomController(IRoomService service, RoomMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<RoomResponse>>> GetAll()
    {
        var items = await _service.GetAllAsync();
        return Ok(items.Select(_mapper.ToResponse).ToList());
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PaginatedResponse<RoomResponse>>> GetPaged([FromQuery] PaginatedRequest request)
    {
        var result = await _service.GetPagedAsync(request.PageNumber, request.PageSize);
        return Ok(result.ToPaginatedResponse(_mapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<RoomResponse>> GetById(Guid id)
    {
        var entity = await _service.GetByIdNotNullAsync(id);
        return Ok(_mapper.ToResponse(entity));
    }

    [HttpPost]
    public async Task<ActionResult<RoomResponse>> Create([FromBody] RoomRequest request)
    {
        var entity = await _service.CreateAsync(_mapper.ToDto(request));
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.ToResponse(entity));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<RoomResponse>> Update(Guid id, [FromBody] RoomRequest request)
    {
        var entity = await _service.UpdateAsync(id, _mapper.ToDto(request));
        return Ok(_mapper.ToResponse(entity));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<RoomResponse>> Delete(Guid id)
    {
        var entity = await _service.DeleteByIdAsync(id);
        return Ok(_mapper.ToResponse(entity));
    }
}
