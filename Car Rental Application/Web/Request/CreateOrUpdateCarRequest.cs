using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Web.Request;

public record CreateOrUpdateCarRequest(
 [Required] string Make,
 [Required] string Model,
 [Required] Category Category,
 [Required] Guid LocationId
    );