using System.ComponentModel.DataAnnotations;

namespace Web.Request;

public record CreateOrUpdateConsultationRequest(
  [Required] DateTime StartTime,
  [Required] DateTime EndTime,
  [Required] Guid RoomId 
    );