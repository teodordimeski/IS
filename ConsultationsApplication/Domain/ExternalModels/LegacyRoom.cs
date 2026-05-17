using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.ExternalModels;

public class LegacyRoom
{
    [Column("RoomCode")]
    public string RoomId { get; set; }  // Maps to "RoomCode" column
    
    [Column("RoomName")]
    public string Name { get; set; }  // Property is "Name", column is "RoomName"
    
    [Column("MaxCapacity")]
    public int Capacity { get; set; }  // Property is "Capacity", column is "MaxCapacity"

}