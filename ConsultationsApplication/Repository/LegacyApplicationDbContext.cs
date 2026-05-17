using Domain.ExternalModels;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class LegacyApplicationDbContext(DbContextOptions<LegacyApplicationDbContext> options) : DbContext(options)
{
    public DbSet<LegacyConsultation> Consultations { get; set; }
    public DbSet<LegacyRoom> Rooms { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LegacyRoom>(e =>
        {
            e.ToTable("RoomDirectory");  // Actual table name
            e.HasKey(v => v.RoomId);   // Using RoomId as primary key
        });
            
        
        modelBuilder.Entity<LegacyConsultation>(e =>
        {
            e.ToTable("ConsultationSlots");  // Actual table name
            e.HasKey(v => v.ConsultationId);
        });    
    }

}