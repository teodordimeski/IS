using Domain.Common;
using Domain.Enums;

namespace Domain.Models;

public class Car : BaseAuditableEntity<CarRentalApplicationUser>
{
    public string Make { get; set; }
    public string Model { get; set; }
    public Category Category { get; set; }
    
    public Guid LocationId { get; set; }
    public virtual Location Location { get; set; }
    
    public virtual ICollection<Rental> Rentals { get; set; }
    public virtual ICollection<Manages> Manages { get; set; }
}