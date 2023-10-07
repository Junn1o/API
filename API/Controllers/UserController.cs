using API.Data;
using API.Models.DTO;
using API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IUserRepository _userRepository;
        public UserController(AppDbContext appDbContext, IUserRepository userRepository)
        {
            _appDbContext = appDbContext;
            _userRepository = userRepository;
        }
        [HttpGet("get-all-user")]
        public IActionResult GetAllUser()
        {
            var userlist = _userRepository.GetAllUser();
            return Ok(userlist);
        }
        [HttpGet("get-user-with-id")]
        public IActionResult GetUserwithId(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
                return NotFound("Data Empty");
        }
        [HttpPost("add-user")]
        public IActionResult AddUser([FromForm] AddUserRequestDTO addUser)
        {
            var userAdd = _userRepository.AddUser(addUser);
            if (userAdd == null)
            {
                return null;
            }
            else
                return Ok(userAdd);
        }
        [HttpPut("update-user-with-id")]
        public IActionResult UpdateUser(int id, [FromForm] AddUserRequestDTO updateUser)
        {
            var userUpdate = _userRepository.UpdateUserById(id, updateUser);
            return Ok(userUpdate);
        }
        [HttpDelete("delete-user-with-id")]
        public IActionResult DeleteUserwithId(int id)
        {
            var userDelete = _userRepository.DeleteUserById(id);
            if (userDelete == null)
            {
                return Ok("User already delete or error");
            }
            else
            {
                return Ok("User deleted");
            }
        }
    }
}
