using Domain.Models;
using Web.Response;

namespace Web.Extensions;

public static class RentalMappingExtensions
{
    public static RentalResponse ToResponse(this Rental rental)
    {
        return new RentalResponse(
            rental.Id,
            rental.UserId,
            rental.User.FirstName,
            rental.User.LastName,
            (rental.EndTime-rental.StartTime).Days
        );
    }

    public static List<RentalResponse> ToResponse(this List<Rental> rental)
    {
        return rental.Select(r => r.ToResponse()).ToList();
    }
}