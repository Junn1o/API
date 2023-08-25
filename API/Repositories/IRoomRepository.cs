using API.Models.Domain;
using API.Models.DTO;
namespace API.Repositories
{
    public interface IRoomRepository
    {
        List<RoomDTO> GetAllRoom();
        RoomwithIdDTO GetRoomwithId(int id);
        List<RoomDTO> GetAllRoomisHire();
        List<RoomDTO> GetAllRoomnotHire();
        List<RoomDTO> GetAllRoomnotApprove();
        List<RoomDTO> GetAllRoomisApprove();
        AddRoomRequestDTO AddRoom(AddRoomRequestDTO addRoom);
        AddRoomRequestDTO UpdateRoom(int id, AddRoomRequestDTO updateRoom);
        Room? DeleteRoomwithId(int id);
    }
}
