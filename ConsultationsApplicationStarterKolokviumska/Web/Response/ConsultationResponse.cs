using System.Runtime.InteropServices.JavaScript;

namespace Web.Response;

public record ConsultationResponse(
 Guid Id,   
 DateOnly ConsultationsDate,
 Guid RoomId,
 String RoomName,
 int RegisteredStudents);