using Domain.Common;

namespace Domain.Models;

public class Rental : BaseAuditableEntity<CarRentalApplicationUser>
{
    public string UserId { get; set; }
    public virtual CarRentalApplicationUser User { get; set; }
    
    public Guid CarId { get; set; }
    public virtual Car Car { get; set; }
    
    public Guid LocationId { get; set; }
    public virtual Location Location { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}