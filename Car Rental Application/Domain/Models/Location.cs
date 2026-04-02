using Domain.Common;

namespace Domain.Models;

public class Location : BaseEntity
{
    public string Name { get; set; }
    public virtual ICollection<Car> Cars { get; set; }
    public virtual ICollection<Rental> Rentals { get; set; }
}