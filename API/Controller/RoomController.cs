using Microsoft.AspNetCore.Mvc;
using API.Data;
using API.Models.DTO;
using API.Repositories;
namespace API.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController: ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IRoomRepository _roomRepository;
        public RoomController(AppDbContext appDbContext, IRoomRepository roomRepository)
        {
            _appDbContext = appDbContext;
            _roomRepository = roomRepository;
        }
        [HttpGet("get-all-room")]
        public IActionResult GetAllRoom()
        {
            var roomlist = _roomRepository.GetAllRoom();
            return Ok(roomlist);
        }
        [HttpGet("get-room-with-id")]
        public IActionResult GetRoomwithId(int id)
        {
            var room = _roomRepository.GetRoomwithId(id);
            if (room !=null)
            {
                return Ok(room);
            }
            else
                return NotFound("Data Empty");
        }
        [HttpPost("add - room")]
        public IActionResult AddRoom([FromBody] AddRoomRequestDTO addRoom)
        {
            var roomAdd = _roomRepository.AddRoom(addRoom);
            return Ok(roomAdd);
        }
        [HttpPut("update-room-with-id")]
        public IActionResult UpdateRoom(int id, [FromBody] AddRoomRequestDTO updateRoom)
        {
            var roomUpdate = _roomRepository.UpdateRoom(id, updateRoom);
            return Ok(roomUpdate);
        }
        [HttpPut("delete-room-with-id")]
        public IActionResult DeleteRoomwithId(int id) 
        {
            var roomDelete = _roomRepository.DeleteRoomwithId(id);
            if (roomDelete == null)
            {
                return StatusCode(500);
            }
            else
            {
                return Ok("Room deleted");
            }
        }
    }
}
