using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Models;

public class CarRentalApplicationUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public Role Role { get; set; }
    public string Phone { get; set; }
    
    public virtual ICollection<Rental> Rentals { get; set; }
    public virtual ICollection<Manages> Manages { get; set; }
}