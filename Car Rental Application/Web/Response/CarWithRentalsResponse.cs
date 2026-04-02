namespace Web.Response;

public class CarWithRentalsResponse
{
    public Guid Id { get; set; }
    public string Category { get; set; }
    public string Make { get; set; }
    public string Model { get; set; }
    public Guid LocationId { get; set; }
    public string LocationName { get; set; }
    public List<RentalResponse> Rentals { get; set; }
}