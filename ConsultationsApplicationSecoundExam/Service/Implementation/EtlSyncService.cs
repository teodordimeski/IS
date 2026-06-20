using Domain.Config;
using Domain.Dto;
using Domain.Models;
using Repository.Interface;
using Service.Interface;

namespace Service.Implementation;

public class EtlSyncService : IEtlSyncService
{
    private readonly IRepository<EtlSyncLog> _etlSyncLogRepository;
    private readonly IConsultationsApiClient<ExternalConsultationsDto> _consultationsApiClient;
    private readonly IConsultationsRepository _consultationsRepository;
    private readonly IRepository<Room> _roomRepository;

    public EtlSyncService(
        IRepository<EtlSyncLog> etlSyncLogRepository,
        IConsultationsApiClient<ExternalConsultationsDto> consultationsApiClient,
        IConsultationsRepository consultationsRepository,
        IRepository<Room> roomRepository)
    {
        _etlSyncLogRepository = etlSyncLogRepository;
        _consultationsApiClient = consultationsApiClient;
        _consultationsRepository = consultationsRepository;
        _roomRepository = roomRepository;
    }

    public async Task SyncAllAsync()
    {
        var log = new EtlSyncLog
        {
            JobName = "ConsultationsSync",
            StartedAt = DateTime.UtcNow,
        };

        try
        {
            var lastRun = await _etlSyncLogRepository.GetAllAsync(
                selector: x => x,
                predicate: x => x.JobName == "ConsultationsSync" && x.Success == true,
                orderBy: x => x.OrderByDescending(v => v.StartedAt));

            var date = lastRun.FirstOrDefault()?.StartedAt ?? DateTime.MinValue;

            var consultationsDto = await _consultationsApiClient.GetAllConsultationsModifiedSinceAsync(date);

            var rooms = await _roomRepository.GetAllAsync(selector: x => x);
            var roomsByName = rooms.ToDictionary(r => r.Name, r => r.Id);

            var consultations = consultationsDto.Items
                .Where(x => roomsByName.ContainsKey(x.RoomName))
                .Select(x => new Consultation
                {
                    Id = GuidHelper.FromLegacyId("Consultation", x.ExternalId),
                    StartTime = x.StartTime,
                    EndTime = x.EndTime,
                    RoomId = roomsByName[x.RoomName]
                })
                .ToList();

            await _consultationsRepository.BulkInsertOrUpdateAsync(consultations);

            log.Success = true;
        }
        catch (Exception ex)
        {
            log.Success = false;
            log.ErrorMessage = ex.Message;
        }
        finally
        {
            log.CompletedAt = DateTime.UtcNow;
            await _etlSyncLogRepository.InsertAsync(log);
        }
    }
}