using Domain.Enums;

namespace Domain.Dto;

public class CarDto
{
    public string Make { get; set; }
    public string Model { get; set; }
    public Category Category { get; set; }
    public Guid LocationId { get; set; }
}