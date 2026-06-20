using System.Text.Json;
using Domain.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Service.Interface;

namespace Web.Controllers;

[ApiController]
[Route("/api/external/attendance")]
[EnableRateLimiting("external-api")]
public class InboundController : ControllerBase
{
    private readonly IInboundEventEntryService _inboundEventEntryService;

    public InboundController(IInboundEventEntryService inboundEventEntryService)
    {
        _inboundEventEntryService = inboundEventEntryService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterAttendanceRequest([FromBody] ConsultationRequestDto request)
    {
        var raw = JsonSerializer.Serialize(request);
        var result = await _inboundEventEntryService.CreateAsync(raw);

        return Accepted(new
        {
            Status = result.Status.ToString(),
            Id = result.Id
        });
    }

    [HttpGet("register/{id}/status")]
    public async Task<IActionResult> GetStatus(Guid id)
    {
        var result = await _inboundEventEntryService.GetVyIdNotNullAsync(id);

        return Ok(new
        {
            Status = result.Status.ToString(),
            Error = result.ErrorMessage,
            Id = result.Id
        });
    }
}