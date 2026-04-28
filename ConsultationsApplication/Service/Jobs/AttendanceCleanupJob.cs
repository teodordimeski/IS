using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Service.Interface;

namespace Service.Jobs;

public class AttendanceCleanupJob : BackgroundService
{
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly ILogger<AttendanceCleanupJob> _logger;

    public AttendanceCleanupJob(IServiceScopeFactory serviceScopeFactory, ILogger<AttendanceCleanupJob> logger)
    {
        _serviceScopeFactory = serviceScopeFactory;
        _logger = logger;
    }
    
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
       _logger.LogInformation("Attendance cleanup job started at: {time}", DateTimeOffset.Now);
       while (!stoppingToken.IsCancellationRequested)
       {
           try
           {
               using var scope = _serviceScopeFactory.CreateScope();
               var attendanceService = scope.ServiceProvider.GetRequiredService<IAttendanceService>();
           
               var toBeDeleted= await attendanceService.GetAllThatShouldBeDeleted(DateTime.UtcNow.AddMonths(-7));
               if (toBeDeleted.Count > 0)
               {
                   _logger.LogInformation("Fetched total of {attendancesCount} attendances", toBeDeleted.Count);
                   foreach (var attendance in toBeDeleted)
                   {
                       _logger.LogInformation("Deleting attendance with id {id}......", attendance.Id);
                       await attendanceService.DeleteByObject(attendance);
                       _logger.LogInformation("Deleted attendance with id {id}", attendance.Id); 
                   }
               }else
               {
                   _logger.LogInformation("No attendacces to be deleted");
               }
           }
           catch (Exception e)
           {
                _logger.LogError(e, "An error occurred while cleaning up attendances");
           }
           await Task.Delay(TimeSpan.FromMinutes(3), stoppingToken);
       }
    }
}