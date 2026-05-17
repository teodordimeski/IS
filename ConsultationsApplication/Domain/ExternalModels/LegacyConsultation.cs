using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ExternalModels;

public class LegacyConsultation 
{
    [Column("SlotId")]
    public int ConsultationId { get; set; }
    
    [Column("SlotStart")]
    public DateTime StartTime { get; set; }  // Property is "StartTime", column is "SlotStart"
    
    [Column("SlotEnd")]
    public DateTime EndTime { get; set; }  // Property is "EndTime", column is "SlotEnd"
    
    [Column("RoomCode")]
    public string RoomId { get; set; }  // Foreign key reference

}