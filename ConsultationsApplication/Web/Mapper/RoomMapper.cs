using Domain.Dto;
using Domain.Models;
using Web.Request;
using Web.Response;

namespace Web.Mapper;

public class RoomMapper
{
    public RoomDto ToDto(RoomRequest request) =>
        new(request.Name, request.Capacity);

    public RoomResponse ToResponse(Room room) =>
        new(room.Id, room.Name, room.Capacity);
}
