using Domain.Common;

namespace Domain.Models;

public class Manages : BaseEntity
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    
    public Guid CarId { get; set; }
    public virtual Car Car { get; set; }
    
    public string UserId { get; set; }
    public virtual CarRentalApplicationUser User { get; set; }
}