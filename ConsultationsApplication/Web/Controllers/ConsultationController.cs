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
public class ConsultationController : ControllerBase
{
    private readonly IConsultationService _service;
    private readonly ConsultationMapper _mapper;

    public ConsultationController(IConsultationService service, ConsultationMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<List<ConsultationResponse>>> GetAll([FromQuery] string? roomName)
    {
        var items = await _service.GetAllAsync(roomName);
        return Ok(items.Select(_mapper.ToResponse).ToList());
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PaginatedResponse<ConsultationResponse>>> GetPaged([FromQuery] PaginatedRequest request)
    {
        var result = await _service.GetPagedAsync(request.PageNumber, request.PageSize);
        return Ok(result.ToPaginatedResponse(_mapper.ToResponse));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ConsultationResponse>> GetById(Guid id)
    {
        var entity = await _service.GetByIdNotNullAsync(id);
        return Ok(_mapper.ToResponse(entity));
    }

    [HttpPost]
    public async Task<ActionResult<ConsultationResponse>> Create([FromBody] ConsultationRequest request)
    {
        var entity = await _service.CreateAsync(_mapper.ToDto(request));
        return CreatedAtAction(nameof(GetById), new { id = entity.Id }, _mapper.ToResponse(entity));
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<ConsultationResponse>> Update(Guid id, [FromBody] ConsultationRequest request)
    {
        var entity = await _service.UpdateAsync(id, _mapper.ToDto(request));
        return Ok(_mapper.ToResponse(entity));
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<ConsultationResponse>> Delete(Guid id)
    {
        var entity = await _service.DeleteByIdAsync(id);
        return Ok(_mapper.ToResponse(entity));
    }
}
