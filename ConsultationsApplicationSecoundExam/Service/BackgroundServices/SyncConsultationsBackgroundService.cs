using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Service.Interface;

namespace Service.BackgroundServices;

public class SyncConsultationsBackgroundService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SyncConsultationsBackgroundService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var etlService = scope.ServiceProvider.GetRequiredService<IEtlSyncService>();
            await etlService.SyncAllAsync();

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
    }
}