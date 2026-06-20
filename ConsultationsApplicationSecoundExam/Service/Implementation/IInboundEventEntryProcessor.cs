using System.Text.Json;
using Domain.Dto;
using Domain.Enums;
using Domain.Models;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class InboundEventEntryProcessor : IInboundEventEntryProcessor
{
    private readonly IAttendanceService _attendanceService;
    private readonly IRepository<InboundEventEntry> _inboundEventEntryRepository;

    public InboundEventEntryProcessor(IAttendanceService attendanceService, IRepository<InboundEventEntry> inboundEventEntryRepository)
    {
        _attendanceService = attendanceService;
        _inboundEventEntryRepository = inboundEventEntryRepository;
    }

    public async Task ProcessPendingEventsAsync()
    {
        var eventsToProcess = await _inboundEventEntryRepository.GetAllAsync(
            selector: x => x,
            predicate: x => x.Status == InboundEventStatus.Pending,
            take: 10);

        foreach (var eventEntry in eventsToProcess)
        {
            await ProcessEventEntry(eventEntry);
        }
    }

    public async Task<Attendance> ProcessEventEntry(InboundEventEntry entry)
    {
        try
        {
            var dto = JsonSerializer.Deserialize<ConsultationRequestDto>(
                entry.RawPayload,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (dto == null || string.IsNullOrEmpty(dto.UserId))
                throw new InvalidOperationException("Invalid or incomplete payload");

            var attendance = await _attendanceService.CreateAsync(new AttendanceDto
            {
                ConsultationId = dto.ConsultationId,
                RoomId = dto.RoomId,
                Comment = dto.Comment,
                UserId = dto.UserId,
            });

            entry.Status = InboundEventStatus.Completed;
            entry.ProcessedAt = DateTime.UtcNow;
            entry.AttendanceId = attendance.Id;
            await _inboundEventEntryRepository.UpdateAsync(entry);

            return attendance;
        }
        catch (Exception ex)
        {
            entry.Status = InboundEventStatus.Failed;
            entry.ErrorMessage = ex.Message;
            await _inboundEventEntryRepository.UpdateAsync(entry);
            return null!;
        }
    }
}