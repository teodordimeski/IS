using System.ComponentModel.DataAnnotations;

namespace Web.Request;

public record ConsultationRequest(
     [Required] Guid RoomId,
     [Required] DateTime StartDate,
     [Required] DateTime EndDate
    );