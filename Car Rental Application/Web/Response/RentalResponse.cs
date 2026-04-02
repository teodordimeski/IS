namespace Web.Response;

public record RentalResponse(
    Guid RentalId,
    string userId,
    string userName,
    string userSurname,
    int daysRented
    );