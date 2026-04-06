using System.ComponentModel.DataAnnotations;

namespace Web.Request;

public record AttendanceRequest(
    [Required]  Guid ConsultationId,
    [Required]  String UserId,
    [Required]  Guid RoomId,
    String? Comment
    );