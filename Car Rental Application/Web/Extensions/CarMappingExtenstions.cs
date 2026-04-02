using Domain.Dto;
using Domain.Models;
using Web.Request;
using Web.Response;

namespace Web.Extensions;

public static class CarMappingExtenstions
{
    public static CarResponse ToResponse(this Car car)
    {
        return new CarResponse(
            car.Id,
            car.Category.ToString(),
            car.Make,
            car.Model,
            car.LocationId,
            car.Location?.Name ?? string.Empty
        );
    }

    public static List<CarResponse> ToResponse(this List<Car> car)
    {
        return car.Select(c => c.ToResponse()).ToList();
    }
    
    public static CarWithRentalsResponse ToResponseWithRentals(this Car car)
    {
        return new CarWithRentalsResponse()
        {
            Id = car.Id,
            Category = car.Category.ToString(),
            Make = car.Make,
            Model = car.Model,
            LocationId = car.LocationId,
            LocationName = car.Location?.Name ?? string.Empty,
            Rentals = car.Rentals.Select(r => r.ToResponse()).ToList()
        };
    }
    
    public static CarDto ToDto(this CreateOrUpdateCarRequest request)
    {
        return new CarDto()
        {
            Make = request.Make,
            Model = request.Model,
            Category = request.Category,
            LocationId = request.LocationId
        };
    }

    public static PaginatedResponse<CarWithRentalsResponse> ToPaginatedResponse(this PaginatedResult<Car> cars)
    {
        return cars.ToPaginatedResponse(c => c.ToResponseWithRentals());
    }
}