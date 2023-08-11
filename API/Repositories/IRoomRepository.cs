using API.Models.Domain;
using API.Models.DTO;
namespace API.Repositories
{
    public interface IRoomRepository
    {
        List<RoomDTO> GetAllRoom();
        RoomwithIdDTO GetRoomwithId();
        Room? DeleteRoomwithId(int id);
    }
}
