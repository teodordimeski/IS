using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Interface;

namespace Service.Implementation;

public class InboundEventProcessorBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var processor = scope.ServiceProvider.GetRequiredService<IInboundEventEntryProcessor>();
            await processor.ProcessPendingEventsAsync();

            
            await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
        }
    }
}