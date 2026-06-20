using System.Text.Json;
using Domain.Enums;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class InboundEventEntryService : IInboundEventEntryService
{
    private readonly IRepository<InboundEventEntry> _repository;

    public InboundEventEntryService(IRepository<InboundEventEntry> repository)
    {
        _repository = repository;
    }

    public async Task<InboundEventEntry> CreateAsync(string rawPayload)
    {
        var inboundEventEntry = new InboundEventEntry()
        {
            RawPayload = rawPayload,
            Status = InboundEventStatus.Pending,
            ReceivedAt = DateTime.UtcNow
        };
        
        return await _repository.InsertAsync(inboundEventEntry);
    }

    public async Task<InboundEventEntry> GetVyIdNotNullAsync(Guid id)
    {
        var result = await _repository.GetAsync(
            selector: x => x,
            predicate: x => x.Id == id);

        if (result == null)
        {
            throw new InvalidOperationException();
        }
        
        return result;
    }
}