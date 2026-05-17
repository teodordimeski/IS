using Domain.Models;
using Repository.Interface;
using EFCore.BulkExtensions;

namespace Repository.Implementation;

public class ConsultationRepository : Repository<Consultation>, IConsultationRepository
{
    

    public ConsultationRepository(ApplicationDbContext context) : base(context)
    {
    }
    
    public async Task BulkInsertOrUpdateConsultationsAsync(List<Consultation> consultations)
    {
        await _context.BulkInsertOrUpdateAsync(consultations);
    }

    public async Task BulkInsertOrUpdateRoomsAsync(List<Room> rooms)
    {
        await _context.BulkInsertOrUpdateAsync(rooms);
    }
}