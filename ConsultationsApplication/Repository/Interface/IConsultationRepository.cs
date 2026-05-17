using Domain.Models;

namespace Repository.Interface;

public interface IConsultationRepository
{
    Task BulkInsertOrUpdateConsultationsAsync(List<Consultation> consultations);
    Task BulkInsertOrUpdateRoomsAsync(List<Room> rooms);
}