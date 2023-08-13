using API.Models.Domain;
using API.Models.DTO;
namespace API.Repositories
{
    public interface IRoomRepository
    {
        List<RoomDTO> GetAllRoom();
        RoomDTO GetRoomwithId(int id);
        AddRoomRequestDTO AddRoom(AddRoomRequestDTO addRoom);
        AddRoomRequestDTO UpdateRoom(int id, AddRoomRequestDTO updateRoom);
        Room? DeleteRoomwithId(int id);
    }
}
