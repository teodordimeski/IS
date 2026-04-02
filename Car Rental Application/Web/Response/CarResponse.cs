namespace Web.Response;

public record CarResponse(
    Guid Id,
    string Category,
    string Make,
    string Model,
    Guid LocationId,
    string LocationName
    );