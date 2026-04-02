using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Rental> Rentals { get; set; }
    public DbSet<Manages> Manages { get; set; }
}